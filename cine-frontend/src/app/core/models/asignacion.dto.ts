// Enviar a POST /api/asignaciones
export interface AsignacionCreateDto {
    idPelicula: number;
    idSalaCine: number;
    fechaPublicacion: string; // 'YYYY-MM-DD'
    fechaFin: string;         // 'YYYY-MM-DD'
  }