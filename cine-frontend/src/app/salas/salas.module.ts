import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SalasRoutingModule } from './salas-routing.module';
import { SalasComponent } from './salas.component';
import { SalaListComponent } from './sala-list/sala-list.component';
import { SalaFormComponent } from './sala-form/sala-form.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SalasComponent,
    SalaListComponent,
    SalaFormComponent
  ],
  imports: [
    CommonModule,
    SalasRoutingModule,
    ReactiveFormsModule
  ]
})
export class SalasModule { }
