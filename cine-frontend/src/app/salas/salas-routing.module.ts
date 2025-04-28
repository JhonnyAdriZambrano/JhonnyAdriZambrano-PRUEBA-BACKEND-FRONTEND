import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalaListComponent } from './sala-list/sala-list.component';
import { SalaFormComponent } from './sala-form/sala-form.component';

const routes: Routes = [
  { 
    path: '', component: SalaListComponent 
  },
  {
    path: 'nuevo',
    component: SalaFormComponent
  },
  {
    path: 'editar/:id',
    component: SalaFormComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalasRoutingModule { }
