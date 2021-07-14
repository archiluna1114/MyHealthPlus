import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import {  Observable , throwError } from 'rxjs';
import { IAppointment } from '../interface/appointmentInterface';
import { IViewAppointment } from '../interface/IViewAppointment';
import { IAppointmentTime } from '../interface/iAppointmentTime';
import { IComment } from '../interface/iComment';

@Injectable({
  providedIn: 'root'
})

export class AppointmentService {
  private url = window.location.origin;
  appointmentData: IAppointment;
  appointmentList: IAppointment[];
  isSuccess: boolean;
  constructor(
    private http: HttpClient

  ) { }

  insertAppointment(appointment: IAppointment) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    this.http.post<IAppointment>(this.url + "/api/Appointment/Insert", appointment, httpOptions).subscribe({
      next: data => {
        this.isSuccess = true;
        return true;
      },
      error: error => {
        //this.errorMessage = error.message;
        //console.error('There was an error!', error);
      }
    });

  }

  insertComment(comment: IComment) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    this.http.post<IComment>(this.url + "/api/Appointment/InsertComment", comment, httpOptions).subscribe({
      next: data => {
        this.isSuccess = true;
        return true;
      },
      error: error => {
        //this.errorMessage = error.message;
        //console.error('There was an error!', error);
      }
    });
  }

  getTodayAppointments(): Observable<IViewAppointment[]> {
    return this.http.get<IViewAppointment[]>(this.url + "/api/Appointment/GetTodayAppointments");
  }

  getAppointmentById(id: any) {
    var response;
    this.http.get<IAppointment>(this.url + "/api/Appointment/GetTodayAppointments?id=" + id)
      .subscribe({
        next: data => {
          data = response
        },
        error: error => {
          //this.errorMessage = error.message;
          //console.error('There was an error!', error);
        }
      });

    return response;
  }

  getAvailableTime(date: any): Observable<IAppointmentTime[]> {
   return this.http.get<IAppointmentTime[]>(this.url + "/api/Appointment/GetAvailableTime?date=" + date);
  }

  getAppointmentByUserId(id: any): Observable<IViewAppointment[]> {
    return this.http.get<IViewAppointment[]>(this.url + "/api/Appointment/GetAppointmentByUserId?id=" + id);
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


