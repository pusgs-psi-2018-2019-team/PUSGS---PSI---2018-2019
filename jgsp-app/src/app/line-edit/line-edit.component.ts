import { Component, OnInit } from '@angular/core';
import { LineEditHttpService } from 'src/services/lineEdit.service';

@Component({
  selector: 'app-line-edit',
  templateUrl: './line-edit.component.html',
  styleUrls: ['./line-edit.component.css']
})
export class LineEditComponent implements OnInit {

  constructor(private http: LineEditHttpService) { }

  ngOnInit() {
  }

}
