import { Component, OnInit } from '@angular/core';
import { LineEditHttpService } from '../../services/lineEdit.service';
import { Line } from '../models/line';
import { AddLine } from '../models/addLine';
import { Station } from '../models/station';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-line-edit',
  templateUrl: './line-edit.component.html',
  styleUrls: ['./line-edit.component.css']
})
export class LineEditComponent implements OnInit {

  constructor(private fb: FormBuilder,private router: Router,private http: LineEditHttpService) { }

  lineForm = this.fb.group({
    SerialNumber: ['', Validators.required],
  });

  stationForm = this.fb.group({
    Name: ['', Validators.required],
    Address: ['', Validators.required],
    X: ['', Validators.required],
    Y: ['', Validators.required],
  });

  selectedLine: string
  selectedStation: string
  lines: string[] = []
  stations: string[] = []
  line: Line = new Line()
  station: Station = new Station()
  StationsAdd: Array<string> = [];
  stationToChose: Array<string> = [];
  stationAddSelected: string
  temp: boolean = true
  serNum: number
  newLine: AddLine = new AddLine()

  ngOnInit() {
    this.http.getAll().subscribe((line) => {
      this.lines = line;
      this.selectedLine = this.lines[0];
      err => console.log(err);
    });    
    this.http.getAllStations().subscribe((data) => {
      this.stationToChose = data;
      this.stationAddSelected = this.stationToChose[0];
      err => console.log(err);
    });
  }

  getSelectedLine(){
    this.http.getLine(this.selectedLine).subscribe((data) => {
      this.line = data;
      this.lineForm.patchValue({ SerialNumber : data.SerialNumber })
      err => console.log(err);
    });
    this.http.getStations(this.selectedLine).subscribe((data) => {
      this.stations = data;
      this.selectedStation = this.stations[0];
      err => console.log(err);
    });
  }

  getSelectedStation(){
    this.http.getSelectedStation(this.selectedStation).subscribe((data) => {
      this.station = data;
      this.stationForm.patchValue({ Name : data.Name, Address : data.Address, X : data.X, Y : data.Y })
      err => console.log(err);
    });
  }

  deleteSelectedLine(){
    this.http.deleteSelectedLine(this.selectedLine).subscribe((data) => {
      if(data == "uspesno")
      {
        alert("Uspesno obrisana linija");
        this.router.navigate(["/lineEdit"]);
      }
      else
      {
        alert("Vec postoji linija sa tim rednim brojem");
        this.router.navigate(["/lineEdit"]);
      }
      err => console.log(err);
    });
  }

  addStation()
  {
    this.StationsAdd.forEach(element => {
      if(element == this.stationAddSelected){
        this.temp = false;
      }
    });
    
    if(this.temp == true)
    {
      this.StationsAdd.push(this.stationAddSelected);
      this.stationAddSelected = null;
    }
    else
    {
      this.temp = true;
    }
  }

  AddLine(){
    this.newLine.SerialNumber = this.serNum;
    this.newLine.StationsAdd = this.StationsAdd;
    this.http.addLine(this.newLine).subscribe((data) => {
      if(data == "uspesno")
      {
        this.newLine = new AddLine();
        this.serNum = null;
        this.StationsAdd = [];
        this.stationAddSelected = this.stationToChose[0];

        alert("Uspesno dodata linija");
        this.router.navigate(["/lineEdit"]);
      }
      else
      {
        this.newLine = new AddLine();
        this.serNum = null;
        this.StationsAdd = [];
        this.stationAddSelected = this.stationToChose[0];

        alert("Neuspesno dodata linija");
        this.router.navigate(["/lineEdit"]);
      }
      err => console.log(err);
    });
  }

}
