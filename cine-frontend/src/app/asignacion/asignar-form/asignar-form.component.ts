import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { forkJoin} from 'rxjs';

import { PeliculaService } from 'src/app/core/services/pelicula.service';
import { PeliculaDto } from 'src/app/core/models/pelicula.dto';
import { SalaCineService} from 'src/app/core/services/sala_cine.service';
import { SalaCineDto } from 'src/app/core/models/sala_cine.dto';
import { AsignacionService } from 'src/app/core/services/asignacion.service';
import { AsignacionCreateDto } from 'src/app/core/models/asignacion.dto';

@Component({
  selector: 'app-asignar-form',
  templateUrl: './asignar-form.component.html',
  styleUrls: ['./asignar-form.component.css']
})
export class AsignarFormComponent implements OnInit {
  asignacionForm!: FormGroup;
  peliculas: PeliculaDto[] = [];
  salas: SalaCineDto[] = [];
  isLoading = true;
  isSaving = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private peliculaService: PeliculaService,
    private salaCineService: SalaCineService,
    private asignacionService: AsignacionService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.loadDropdownData();
  }

  initForm(): void {
    this.asignacionForm = this.fb.group({
      idPelicula: [null, Validators.required],
      idSalaCine: [null, Validators.required],
      fechaPublicacion: ['', Validators.required],
      fechaFin: ['', Validators.required]
    });
  }

  loadDropdownData(): void {
    this.isLoading = true;
    this.errorMessage = null;
    forkJoin({
      peliculas: this.peliculaService.getPeliculas(),
      salas: this.salaCineService.getSalas()
    }).subscribe({
      next: (results) => {
        this.peliculas = results.peliculas;
        this.salas = results.salas;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error cargando datos para asignación:', err);
        this.errorMessage = 'No se pudieron cargar las opciones de películas o salas.';
        this.isLoading = false;
      }
    });
  }
  onSubmit(): void {
    this.errorMessage = null;
    this.successMessage = null;

    if (this.asignacionForm.invalid) {
      this.asignacionForm.markAllAsTouched();
      return;
    }

    this.isSaving = true;

    const asignacionData: AsignacionCreateDto = {
      idPelicula: this.asignacionForm.value.idPelicula,
      idSalaCine: this.asignacionForm.value.idSalaCine,
      fechaPublicacion: this.asignacionForm.value.fechaPublicacion,
      fechaFin: this.asignacionForm.value.fechaFin
    };

    this.asignacionService.crearAsignacion(asignacionData).subscribe({
      next: (response) => {
        console.log('Asignación creada:', response);
        this.successMessage = '¡Película asignada a la sala correctamente!';
        this.resetForm(); //Limpia
        this.isSaving = false;
      },
      error: (err) => {
        console.error('Error creando asignación:', err);
        this.errorMessage = `Error al asignar: ${err.error?.message || err.message || 'Error desconocido'}`;
        this.isSaving = false;
      },
       complete: () => this.isSaving = false
    });
  }

  //Limpiar el formulario
  resetForm(): void {
    this.asignacionForm.reset();
    this.successMessage = null;
    this.errorMessage = null;
  }
}
