import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

const authUrl = environment.authUrl;

@Injectable()
export class TokenHttpInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {

  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const securityToken = this.authService.getToken();
    if (request.url === authUrl) {
      request.headers.set('Content-Type', 'application/json; charset=UTF-8');
      return next.handle(request);
    } else if (securityToken.length > 0) {
      request = request.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': `Bearer ${securityToken}`
        }
      });
      return next.handle(request);
    }
    else{
      return next.handle(request);
    }
  }
}
