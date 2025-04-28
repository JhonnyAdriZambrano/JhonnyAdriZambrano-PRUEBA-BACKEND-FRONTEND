import { Component, OnInit } from '@angular/core';
import { PeliculaService} from '../../core/services/pelicula.service';
import { PeliculaDto } from 'src/app/core/models/pelicula.dto';
@Component({
  selector: 'app-pelicula-list',
  templateUrl: './pelicula-list.component.html',
  styleUrls: ['./pelicula-list.component.css']
})
export class PeliculaListComponent implements OnInit {
  peliculas: PeliculaDto[] = [];
  isLoading = true;
  errorMessage: string | null = null;

  constructor(private peliculaService: PeliculaService ) { }

  ngOnInit(): void {
    this.loadPeliculas();
  }
   // Método para cargar las películas
   loadPeliculas(): void {
    this.isLoading = true;
    this.errorMessage = null;
    this.peliculaService.getPeliculas().subscribe({
      next: (data) => {
        this.peliculas = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error cargando películas:', err);
        this.errorMessage = 'No se pudieron cargar las películas.';
        this.isLoading = false;
      }
    });
  }
  // Método para borrar
  deletePelicula(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta película?')) {
      this.isLoading = true;
      this.peliculaService.deletePelicula(id).subscribe({
        next: () => {
          console.log(`Película con ID ${id} eliminada (lógicamente)`);
          this.loadPeliculas();
        },
        error: (err) => {
          console.error(`Error eliminando película ${id}:`, err);
          this.errorMessage = `No se pudo eliminar la película. ${err.message || ''}`;
          this.isLoading = false;
        }
      });
    }
  }
}
