import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SalaStatusDto, DashboardStatsDto} from '../models/sala_cine.dto';

import { SalaCineDto, SalaCineCreateDto, SalaCineUpdateDto } from '../models/sala_cine.dto';

@Injectable({
  providedIn: 'root'
})
export class SalaCineService {
  private baseUrl = `${environment.apiUrl}/salas`; // Ruta base para salas

  constructor(private http: HttpClient) { }

  getStatusByName(nombreSala: string): Observable<SalaStatusDto> {
    const params = new HttpParams().set('nombreSala', nombreSala);
    return this.http.get<SalaStatusDto>(`${this.baseUrl}/statusPorNombre`, { params: params });
  }

  getSalas(): Observable<SalaCineDto[]> {
    return this.http.get<SalaCineDto[]>(this.baseUrl);
  }

  getSalaById(id: number): Observable<SalaCineDto> {
    return this.http.get<SalaCineDto>(`<span class="math-inline">\{this\.baseUrl\}/</span>{id}`);
  }

  createSala(salaData: SalaCineCreateDto): Observable<SalaCineDto> {
    return this.http.post<SalaCineDto>(this.baseUrl, salaData);
  }

  updateSala(id: number, salaData: SalaCineUpdateDto): Observable<void> {
    return this.http.put<void>(`<span class="math-inline">\{this\.baseUrl\}/</span>{id}`, salaData);
  }

  deleteSala(id: number): Observable<void> {
    const url = `${this.baseUrl}/${id}`;
    console.log('Intentando borrar sala en URL:', url);
    return this.http.delete<void>(url);
  }
  getDashboardStats(): Observable<DashboardStatsDto> {
    return this.http.get<DashboardStatsDto>(`${this.baseUrl}/stats`);
  }
}
