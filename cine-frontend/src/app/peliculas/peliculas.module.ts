import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { PeliculasRoutingModule } from './peliculas-routing.module';
import { PeliculasComponent } from './peliculas.component';
import { PeliculaListComponent } from './pelicula-list/pelicula-list.component';
import { PeliculaFormComponent } from './pelicula-form/pelicula-form.component';


@NgModule({
  declarations: [
    PeliculasComponent,
    PeliculaListComponent,
    PeliculaFormComponent
  ],
  imports: [
    CommonModule,
    PeliculasRoutingModule,
    ReactiveFormsModule
  ]
})
export class PeliculasModule { }
