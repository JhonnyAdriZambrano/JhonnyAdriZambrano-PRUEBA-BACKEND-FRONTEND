import { Component, OnInit } from '@angular/core';
import { PeliculaService } from '../../core/services/pelicula.service';
import { SalaCineService } from '../../core/services/sala_cine.service';
@Component({
  selector: 'app-dashboard-view',
  templateUrl: './dashboard-view.component.html',
  styleUrls: ['./dashboard-view.component.css']
})
export class DashboardViewComponent implements OnInit {
totalPeliculas: number | null = null;
totalSalas: number | null = null;
totalSalasDisponibles: number | null = null;
isLoading = true;
errorMessage: string | null = null;

constructor(
  private peliculaService: PeliculaService,
  private salaCineService: SalaCineService
) { }

ngOnInit(): void {
  this.loadDashboardData();
}

loadDashboardData(): void {
  this.isLoading = true;
  this.errorMessage = null;
  this.salaCineService.getDashboardStats().subscribe({
      next: (stats) => {
      this.totalPeliculas = stats.totalPeliculasActivas;
      this.totalSalas = stats.totalSalasActivas;
      this.totalSalasDisponibles = stats.totalSalasDisponibles;
      this.isLoading = false;
    },
    error: (err) => {
      console.error('Error cargando datos del dashboard:', err);
      this.errorMessage = 'No se pudieron cargar los datos del dashboard.';
      this.isLoading = false;
    }
  });
}
}