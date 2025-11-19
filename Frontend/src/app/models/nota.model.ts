export interface Nota {
  id: number;
  nombre: string;
  idProfesor: number;
  nombreProfesor: string;
  idEstudiante: number;
  nombreEstudiante: string;
  valor: number;
}

export interface CreateNota {
  nombre: string;
  idProfesor: number;
  idEstudiante: number;
  valor: number;
}
