import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbDateStruct,ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {  Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { IAppointment } from '../interface/appointmentInterface';
import { User } from '../models';
import { AppointmentService } from '../service/appointment.service';
import { AuthenticationService } from '../service/authentication.service';
import { map } from 'rxjs/operators';
import { IViewAppointment } from '../interface/IViewAppointment';
import { IAppointmentTime } from '../interface/iAppointmentTime';
import { IComment } from '../interface/iComment';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  @Input() appointmentData: IAppointment;
  @Input() commentData: IComment;

  currentUser: User;
  roleId: number;
  currentUserId: number;
  closeResult: string;
  dropDownValue: string;
  model: NgbDateStruct;
  filteredDate: NgbDateStruct;
  timeValue: number;
  formData: FormGroup;
  formComment: FormGroup;
  openedAppointmentId: number;
  public appointments: IViewAppointment[];
  public appointmentTimes: IAppointmentTime[];
  userSubmitted: boolean;

  constructor(private modalService: NgbModal,
    private fb: FormBuilder,
    private appointmentService: AppointmentService,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

    var text = localStorage.getItem("currentUser");
    var obj = JSON.parse(text);
    this.currentUserId = obj.Id;
    this.roleId = obj.RoleId;

    if (this.roleId < 3) {
      this.appointmentService.getTodayAppointments().subscribe(data => {
        this.appointments = data;
      });
    }
    else {
      this.appointmentService.getAppointmentByUserId(obj.Id).subscribe(data => {
        console.log(data);
        this.appointments = data;
      });
    }
  }



  ngOnInit(): void {
    let today = new Date();
    let mindate = new Date();
    this.dropDownValue = "Select Type of Appointment";
    mindate.setDate(today.getDate() - 1);

    this.filteredDate = {
      year: mindate.getFullYear(),
      month: mindate.getMonth() +1,
      day: mindate.getDate()+1
    }

    this.createFormData();
    this.createFormCommentData();

  }

  createFormData() {
    this.formData = this.fb.group({
      appointmentType: [null, Validators.required],
      appointmentDate: [null, Validators.required],
      appointmentTime: [null, Validators.required],
         description: [null],
    });
  }

  createFormCommentData() {
    this.formComment = this.fb.group({
      comment:[null]
    })
  }

  apppointmentDataHandler() {
    var text = localStorage.getItem("currentUser");
    var obj = JSON.parse(text);

    return this.appointmentData = {
      appointmentId:0,
      appointmentTypeId: this.dropDownDataSelector(this.appointmentType.value),
      appointmentDate: this.dateFormatter(this.appointmentDate.value),
      appointmentTimeId: this.appointmentTime.value,
      description: "N/A",
      patientId: obj.Id
    }
  }


  commentDataHandler(index) {
    var today = new Date();
    var dd = String(today.getDate()).length == 1 ? "0" + String(today.getDate()) : String(today.getDate());
    var mm = String(today.getMonth() + 1).length == 1 ? "0" + String(today.getMonth() + 1) : String(today.getMonth() + 1); //January is 0!
    var yyyy = today.getFullYear();

    var date = mm + '/' + dd + '/' + yyyy;

    return this.commentData = {
      CommentId: 0,
      AppointmentId: index,
      Comment: this.comment.value,
      CommentDate: date
    }
  }

  get appointmentType() {
    return this.formData.get('appointmentType') as FormControl;
  }

  get appointmentDate() {
    return this.formData.get('appointmentDate') as FormControl;
  }

  get appointmentTime() {
    return this.formData.get('appointmentTime') as FormControl;
  }

  get description() {
    return this.formData.get('description') as FormControl;
  }

  get comment() {
    return this.formComment.get('comment') as FormControl;
  }

  open(content) {
    var today = new Date();
    var dd = String(today.getDate()).length == 1 ? "0" + String(today.getDate()) : String(today.getDate());
    var mm = String(today.getMonth() + 1).length == 1 ? "0" + String(today.getMonth() + 1) : String(today.getMonth() + 1); //January is 0!
    var yyyy = today.getFullYear();

    var date = mm + '/' + dd + '/' + yyyy;

    this.appointmentService.getAvailableTime(date).subscribe({
      next: data => {
        this.appointmentTimes = data;
        return true;
      },
      error: error => {
        //this.errorMessage = error.message;
        //console.error('There was an error!', error);
      }
    });

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;

    });
    setTimeout(() => {
      for (var i = 0; i < this.appointmentTimes.length; i++) {
        console.log(document.getElementById("btnTime1"));
        console.log(this.appointmentTimes[i].AppointmentTimeId);
        document.getElementById("btnTime" + this.appointmentTimes[i].AppointmentTimeId).setAttribute("disabled","true")
      }
    },1500);
  }

  private dropDownDataSelector(string) {
    if (string === "General Check-Ups") {
      return 1;
    }

    if (string === "Skin Cancer Checks") {
      return 2;
    }

    return 
  }

  private dateFormatter(string) {
    if (string != null) {
      var month = string.month.toString().length == 1 ? "0" + string.month : string.month;
      var day = string.day.toString().length == 1 ? "0" + string.day : string.day;

      return month + "/" + day + "/" + string.year;
    }
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  getAppointmentList() {
    var text = localStorage.getItem("currentUser");
    var obj = JSON.parse(text);
    this.appointmentService.getAppointmentByUserId(obj.Id).subscribe(data => {
      this.appointments = data;
      }
    );
  }

  onSelectFromDropdown(value) {
    this.dropDownValue = value;
  }

  onSelectTime(time) {
    this.timeValue = time;
    
  }

  onSubmit() {
    console.log(this.formData.valid);
    this.userSubmitted = true;
    if (this.formData.valid) {
       this.appointmentService.insertAppointment(this.apppointmentDataHandler());

       this.appointmentService.getAppointmentByUserId(this.currentUserId).subscribe(data => {
         console.log(data);
         this.appointments = data;
       });
    }
  }

  onButtonGroupClick($event) {
    let clickedElement = $event.target || $event.srcElement;

    if (clickedElement.nodeName === "BUTTON") {

      let isCertainButtonAlreadyActive = clickedElement.parentElement.querySelector(".active");
      // if a Button already has Class: .active
      if (isCertainButtonAlreadyActive) {
        isCertainButtonAlreadyActive.classList.remove("active");
      }

      clickedElement.className += " active";
    }

  }

  onAddCommentCLick(content,index) {
    this.modalService.open(content, { ariaLabelledBy: 'comment-modal' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;

    });
    this.openedAppointmentId = index;
  }

  onCommentSubmit(index) {
    if (this.formComment.valid) {
      this.appointmentService.insertComment(this.commentDataHandler(index));

      this.appointmentService.getTodayAppointments().subscribe(data => {
        this.appointments = data;
      });
    }
  }
}
