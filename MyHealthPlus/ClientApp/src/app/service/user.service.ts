import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IUser } from '../interface/userInterface';


@Injectable({
  providedIn: 'root'
})


export class UserService {
  private url = window.location.origin;
  constructor(
    private http: HttpClient

  ) { }

  insertUser(user: IUser){
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      }
    this.http.post<IUser>(this.url + "/api/User/Register", user, httpOptions).subscribe({
      next: data => {
        return true;
      },
      error: error => {
        //this.errorMessage = error.message;
        //console.error('There was an error!', error);
      }
    });

  }

  getUser() {

  }

  getAll() {

  }

  updateUser() {

  }

  deleteUser() {

  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}
