import { Component, OnInit } from '@angular/core';
import { SalaCineDto } from 'src/app/core/models/sala_cine.dto';
import { SalaCineService } from 'src/app/core/services/sala_cine.service';
@Component({
  selector: 'app-sala-list',
  templateUrl: './sala-list.component.html',
  styleUrls: ['./sala-list.component.css']
})
export class SalaListComponent implements OnInit {
  
  salas: SalaCineDto[] = [];
  isLoading = true;
  errorMessage: string | null = null;

  constructor( private salaCineService: SalaCineService) { }

  ngOnInit(): void {
    this.loadSalas();
  }

  loadSalas(): void {
    this.isLoading = true;
    this.errorMessage = null;
    //devuelve SalaCineDto[]
    this.salaCineService.getSalas().subscribe({
      next: (data) => {
        this.salas = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error cargando salas:', err);
        this.errorMessage = 'No se pudieron cargar las salas.';
        this.isLoading = false;
      }
    });
  }
  deleteSala(id: number): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta sala? (Eliminación Lógica)')) {
      this.isLoading = true;
      //borrado lógico en backend
      this.salaCineService.deleteSala(id).subscribe({
        next: () => {
          console.log(`Sala con ID ${id} eliminada (lógicamente)`);
          this.loadSalas();
        },
        error: (err) => {
          console.error(`Error eliminando sala ${id}:`, err);
          this.errorMessage = `No se pudo eliminar la sala. ${err.message || ''}`;
          this.isLoading = false;
        }
      });
    }
  }
}
