import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AsignacionRoutingModule } from './asignacion-routing.module';
import { AsignarFormComponent } from './asignar-form/asignar-form.component';

@NgModule({
  declarations: [
    AsignarFormComponent
  ],
  imports: [
    CommonModule,
    AsignacionRoutingModule,
    ReactiveFormsModule
  ]
})
export class AsignacionModule { }
