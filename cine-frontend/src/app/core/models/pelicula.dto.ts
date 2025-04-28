
//Devuelve GET /api/peliculas{id}
export interface PeliculaDto {
    idPelicula: number;
    nombre: string;
    duracion: number | null;
  }

//Enviar a POST /api/peliculas
export interface PeliculaCreateDto {
    nombre: string;
    duracion: number | null;
  }

//Enavia a PUT /api/peliculas{id}
export interface PeliculaUpdateDto {
    nombre: string;
    duracion: number | null;
  }
