import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {
  path: '',
  component: LayoutComponent,
  children: [
    {
      path: 'dashboard', // /app/dashboard
      loadChildren: () => import('../dashboard/dashboard.module').then(m => m.DashboardModule)
    },
    {
      path: 'peliculas', // /app/peliculas
      loadChildren: () => import('../peliculas/peliculas.module').then(m => m.PeliculasModule)
    },
    {
      path: 'salas', // /app/salas
      loadChildren: () => import('../salas/salas.module').then(m => m.SalasModule)
    },
    {
      path: 'asignar', // /app/asignar
       loadChildren: () => import('../asignacion/asignacion.module').then(m => m.AsignacionModule)
    },
    // /app () a /app/dashboard
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
  ]
},
  { path: 'dashboard', loadChildren: () => import('../dashboard/dashboard.module').then(m => m.DashboardModule) },
  { path: 'peliculas', loadChildren: () => import('../peliculas/peliculas.module').then(m => m.PeliculasModule) },
  { path: 'salas', loadChildren: () => import('../salas/salas.module').then(m => m.SalasModule) },
  { path: 'asignar', loadChildren: () => import('../asignacion/asignacion.module').then(m => m.AsignacionModule) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
