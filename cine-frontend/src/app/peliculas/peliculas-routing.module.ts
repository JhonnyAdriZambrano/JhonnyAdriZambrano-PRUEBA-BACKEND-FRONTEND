import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PeliculaListComponent } from './pelicula-list/pelicula-list.component';
import { PeliculaFormComponent } from './pelicula-form/pelicula-form.component';

const routes: Routes = [{ path: '', component: PeliculaListComponent },
  {
    path: 'nuevo',
    component: PeliculaFormComponent
  },
  {
    path: 'editar/:id',
    component: PeliculaFormComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PeliculasRoutingModule { }
