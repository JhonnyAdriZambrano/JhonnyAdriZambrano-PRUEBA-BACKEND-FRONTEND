import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree,Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
    console.log('AuthGuard: Verificando acceso.', '| Usuario logueado?:', isLoggedIn, '| Intentando acceder a:', state.url);

    if (isLoggedIn) {
      return true;
    } else {
      console.warn('AuthGuard: Acceso denegado. Redirigiendo a /login...');
      this.router.navigate(['/login']);
      return false;
    }
  }
}
