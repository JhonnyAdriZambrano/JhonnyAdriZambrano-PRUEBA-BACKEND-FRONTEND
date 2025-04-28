import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PeliculaService } from 'src/app/core/services/pelicula.service';
import { PeliculaCreateDto, PeliculaUpdateDto, PeliculaDto } from 'src/app/core/models/pelicula.dto';
@Component({
  selector: 'app-pelicula-form',
  templateUrl: './pelicula-form.component.html',
  styleUrls: ['./pelicula-form.component.css']
})
export class PeliculaFormComponent implements OnInit {

  peliculaForm!: FormGroup;
  isEditMode: boolean = false;
  peliculaId: number | null = null;
  isLoading: boolean = false;
  isSaving: boolean = false;
  errorMessage: string | null = null;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private peliculaService: PeliculaService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.peliculaId = +id;
        this.loadPeliculaData();
      } else {
        this.isEditMode = false;
      }
    });
  }

  initForm(): void {
    this.peliculaForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.maxLength(200)]],
      duracion: [null, [Validators.min(1), Validators.max(999)]]
    });
  }

  loadPeliculaData(): void {
    if (!this.peliculaId) return;
    this.isLoading = true;
    this.errorMessage = null;
    this.peliculaService.getPeliculaById(this.peliculaId).subscribe({
      next: (pelicula) => {
        if (pelicula) {
          this.peliculaForm.patchValue(pelicula);
        } else {
          this.errorMessage = 'Película no encontrada.';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error(`Error cargando película ${this.peliculaId}:`, err);
        this.errorMessage = 'No se pudieron cargar los datos de la película.';
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    this.errorMessage = null; //Limpia

    if (this.peliculaForm.invalid) {
      this.peliculaForm.markAllAsTouched();
      return;
    }
    this.isSaving = true;
    const peliculaData: PeliculaCreateDto | PeliculaUpdateDto = {
      nombre: this.peliculaForm.value.nombre,
      duracion: this.peliculaForm.value.duracion || null
    };

    if (this.isEditMode && this.peliculaId !== null) {
      //ACTUALIZAR/PUT
      this.peliculaService.updatePelicula(this.peliculaId, peliculaData as PeliculaUpdateDto).subscribe({
        next: () => {
          console.log(`Película ${this.peliculaId} actualizada`);
          this.router.navigate(['/app/peliculas']); //Vuelve a la lista
        },
        error: (err) => {
          console.error(`Error actualizando película ${this.peliculaId}:`, err);
          this.errorMessage = `Error al actualizar: ${err.error?.message || err.message || 'Error desconocido'}`;
          this.isSaving = false;
        },
         complete: () => this.isSaving = false
      });
    } else {
      //CREAR/POST
      this.peliculaService.createPelicula(peliculaData as PeliculaCreateDto).subscribe({
        next: (peliculaCreada) => {
          console.log(`Película creada con ID: ${peliculaCreada.idPelicula}`);
          this.router.navigate(['/app/peliculas']); //Vuelve a la lista
        },
        error: (err) => {
          console.error(`Error creando película:`, err);
           this.errorMessage = `Error al crear: ${err.error?.message || err.message || 'Error desconocido'}`;
          this.isSaving = false;
        },
         complete: () => this.isSaving = false
      });
    }
  }

}
