import { Component, OnInit } from '@angular/core';
import { PeliculaService } from '../core/services/pelicula.service';
import { SalaCineService } from '../core/services/sala_cine.service'
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  totalPeliculas = 0;
  totalSalas = 0;
  totalSalasDisponibles = 0;

  constructor(
    private peliculaService: PeliculaService,
    private salaCineService: SalaCineService) { }

  ngOnInit(): void {
    this.loadIndicadores();
  }
  
  loadIndicadores(): void {
    // Cargar total películas
    this.peliculaService.getPeliculas().subscribe(peliculas => {
      this.totalPeliculas = peliculas.length;
    });

    // Cargar total salas
    this.salaCineService.getSalas().subscribe(salas => {
      this.totalSalas = salas.length;
    });
  }
}
