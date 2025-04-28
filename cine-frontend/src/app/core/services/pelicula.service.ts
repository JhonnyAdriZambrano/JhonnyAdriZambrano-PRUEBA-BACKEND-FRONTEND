import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

import { PeliculaDto, PeliculaCreateDto, PeliculaUpdateDto } from '../models/pelicula.dto';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {

  private baseUrl = `${environment.apiUrl}/peliculas`;

  constructor(private http: HttpClient) { }

  // GET /api/peliculas
  getPeliculas(): Observable<PeliculaDto[]> {
    return this.http.get<PeliculaDto[]>(this.baseUrl);
  }

  // GET /api/peliculas/{id}
  getPeliculaById(id: number): Observable<PeliculaDto> {
    return this.http.get<PeliculaDto>(`<span class="math-inline">\{this\.baseUrl\}/</span>{id}`); //// Añade el ID a la URL base
  }

  // POST /api/peliculas
  createPelicula(peliculaData: PeliculaCreateDto): Observable<PeliculaDto> {
    return this.http.post<PeliculaDto>(this.baseUrl, peliculaData);
  }

  // PUT /api/peliculas/{id}
  updatePelicula(id: number, peliculaData: PeliculaUpdateDto): Observable<void> {
    return this.http.put<void>(`<span class="math-inline">\{this\.baseUrl\}/</span>{id}`, peliculaData);
  }

  // DELETE /api/peliculas/{id}
  deletePelicula(id: number): Observable<void> {

    const url = `${this.baseUrl}/${id}`;
    console.log('Intentando borrar película en URL:', url);
    return this.http.delete<void>(url);
  }

  // GET /api/peliculas/buscarPorNombre?nombre=
  searchPeliculasByName(nombre: string): Observable<PeliculaDto[]> {
    // Crea parámetros de consulta
    const params = new HttpParams().set('nombre', nombre);
    // Petición GET
    return this.http.get<PeliculaDto[]>(`${this.baseUrl}/buscarPorNombre`, { params: params });
  }

  // GET /api/peliculas/porFechaPublicacion?fecha=
  getPeliculasByFecha(fecha: string): Observable<PeliculaDto[]> {
    // formato YYYY-MM-DD
    const params = new HttpParams().set('fecha', fecha);
    return this.http.get<PeliculaDto[]>(`${this.baseUrl}/porFechaPublicacion`, { params: params });
  }
}

