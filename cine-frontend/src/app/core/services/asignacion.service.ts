import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AsignacionCreateDto } from '../models/asignacion.dto';

@Injectable({
  providedIn: 'root'
})
export class AsignacionService {
  private baseUrl = `${environment.apiUrl}/asignaciones`;

  constructor(private http: HttpClient) { }
  // POST /api/asignaciones
  crearAsignacion(asignacionData: AsignacionCreateDto): Observable<any> {
    return this.http.post<any>(this.baseUrl, asignacionData);
  }
}