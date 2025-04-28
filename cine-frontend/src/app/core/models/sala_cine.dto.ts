//datos de una Sala de Cine que recibimos de la API
export interface SalaCineDto {
  idSala: number;
  nombre: string;
  estado: string | null;
}

// POST /api/salas
export interface SalaCineCreateDto {
  nombre: string;
  estado: string | null;
}

// PUT /api/salas/{id})
export interface SalaCineUpdateDto {
  nombre: string;
  estado: string | null;
}

export interface SalaStatusDto {
  salaNombre: string;
  status: string;
  peliculasCount: number;
}

export interface DashboardStatsDto {
  totalPeliculasActivas: number;
  totalSalasActivas: number;
  totalSalasDisponibles: number;
}