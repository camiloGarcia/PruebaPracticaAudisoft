import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Estudiantes } from './components/estudiantes/estudiantes';
import { Profesores } from './components/profesores/profesores';
import { Notas } from './components/notas/notas';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'estudiantes', component: Estudiantes },
  { path: 'profesores', component: Profesores },
  { path: 'notas', component: Notas },
  { path: '**', redirectTo: '' }
];
