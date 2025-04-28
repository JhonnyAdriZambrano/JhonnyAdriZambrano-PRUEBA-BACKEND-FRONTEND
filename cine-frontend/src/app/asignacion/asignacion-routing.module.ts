import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AsignarFormComponent } from './asignar-form/asignar-form.component';

const routes: Routes = [{ path: '', component: AsignarFormComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AsignacionRoutingModule { }
