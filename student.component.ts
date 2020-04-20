import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Student } from '../shared/models/student.model';


@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  title = 'Student GUI';
  
  constructor(private _httpService: Http) { }

  mainurl: string='http://localhost:44600/api/Student';
  hosturl : string ='http://127.0.0.1:5000/StudentsMicroservice/api/student';
  //mainurl : string = this.hosturl;
  accessPointUrlStud: string = this.mainurl ;//'http://localhost:44390/api/Student';
  apiValues: string[] = [];
  accessPointUrl :string = this.mainurl + '/GetStudentById/1';//'http://localhost:44390/api/Student/GetStudentById/1';
  Students : Student;
 
  searchedStudents : Student; 
  isPublish : boolean = true;

  ngOnInit() 
  {
    
    if(this.isPublish)
    {
        this.accessPointUrl = this.hosturl + '/GetStudentById/1'
        this._httpService.get(this.accessPointUrl).subscribe(x => {
        this.Students = x.json() as Student;
        });
    }
    else
    {
    //this.accessPointUrl = this.hosturl + '/GetStudentById/1'
        this._httpService.get(this.accessPointUrl).subscribe(x => {
        this.Students = x.json() as Student;
        });
    }
    this.getAllStudent();
  }

  getAllStudent()
  {
    if(this.isPublish)
    {
      console.log(this.hosturl);
      this._httpService.get(this.hosturl).subscribe(values => {
      this.apiValues = values.json() as string[];
      });
    }
    else
    {
      console.log(this.mainurl);
      this._httpService.get(this.mainurl).subscribe(values => {
      this.apiValues = values.json() as string[];
      });
    }
  }


  search(searchStudent : string)
  {
    if (searchStudent !== '')
    {

      if(this.isPublish)
      {
        var accessPointUrlnew: string =this.hosturl + "/GetStudentById/" + searchStudent;
        this._httpService.get(accessPointUrlnew).subscribe(x => {
          this.searchedStudents = x.json() as Student;
        });
      }
      else
      {
      //var accessPointUrlnew: string ="http://localhost:44390/api/Student/GetStudentById/" + searchStudent;
        var accessPointUrlnew: string =this.mainurl + "/GetStudentById/" + searchStudent;
        this._httpService.get(accessPointUrlnew).subscribe(x => {
        this.searchedStudents = x.json() as Student;
        });
      }
    }
  }

  onSubmit(student: Student)
  {
    if(this.isPublish)
    {
        this._httpService.post(this.hosturl , student).subscribe(status=> console.log(JSON.stringify(status)));
    }
    else
    {
        this._httpService.post(this.mainurl , student).subscribe(status=> console.log(JSON.stringify(status)));
    }
    this.getAllStudent();
  }


}
