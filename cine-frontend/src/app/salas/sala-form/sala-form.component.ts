import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SalaCineService } from 'src/app/core/services/sala_cine.service';
import { SalaCineCreateDto, SalaCineUpdateDto } from 'src/app/core/models/sala_cine.dto';
@Component({
  selector: 'app-sala-form',
  templateUrl: './sala-form.component.html',
  styleUrls: ['./sala-form.component.css']
})
export class SalaFormComponent implements OnInit {
  salaForm!: FormGroup;
  isEditMode = false;
  salaId: number | null = null;
  isLoading = false;
  isSaving = false;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private salaCineService: SalaCineService
  ) { }

  ngOnInit(): void {
    this.initForm();
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.salaId = +idParam;
      this.loadSalaData();
    }
  }
  initForm(): void {
    this.salaForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.maxLength(100)]],
      estado: [null, [Validators.maxLength(50)]]
    });
  }
  loadSalaData(): void {
    if (!this.salaId) return;

    this.isLoading = true;
    this.errorMessage = null;
    this.salaCineService.getSalaById(this.salaId).subscribe({
      next: (sala) => {
        if (sala) {
          this.salaForm.patchValue(sala);
        } else {
           this.errorMessage = 'Sala no encontrada.';
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error(`Error cargando sala ${this.salaId}:`, err);
        this.errorMessage = 'No se pudieron cargar los datos de la sala.';
        this.isLoading = false;
      }

    });
  }

  onSubmit(): void {
    this.errorMessage = null;

    if (this.salaForm.invalid) {
      this.salaForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    const salaData: SalaCineCreateDto | SalaCineUpdateDto = {
       nombre: this.salaForm.value.nombre,
       estado: this.salaForm.value.estado
    };
    if (this.isEditMode && this.salaId) {

      this.salaCineService.updateSala(this.salaId, salaData as SalaCineUpdateDto).subscribe({
        next: () => {
          console.log(`Sala ${this.salaId} actualizada`);
          this.router.navigate(['/app/salas']); 
        },
        error: (err) => {
          console.error(`Error actualizando sala ${this.salaId}:`, err);
          this.errorMessage = `Error al actualizar: ${err.error?.message || err.message || 'Error desconocido'}`;
          this.isSaving = false;
        },
        complete: () => this.isSaving = false
      });

    } else {
      this.salaCineService.createSala(salaData as SalaCineCreateDto).subscribe({
         next: (salaCreada) => {
           console.log(`Sala creada con ID: ${salaCreada.idSala}`);
           this.router.navigate(['/app/salas']);
         },
         error: (err) => {
           console.error(`Error creando sala:`, err);
           this.errorMessage = `Error al crear: ${err.error?.message || err.message || 'Error desconocido'}`;
           this.isSaving = false;
         },
         complete: () => this.isSaving = false
      });
    }
  }
}
