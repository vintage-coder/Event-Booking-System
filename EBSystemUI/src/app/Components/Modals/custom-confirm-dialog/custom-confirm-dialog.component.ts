import { Component, Inject, OnInit } from '@angular/core';
import {ConfirmDialogData} from './../../../Models/confirm-dialog-data';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-custom-confirm-dialog',
  templateUrl: './custom-confirm-dialog.component.html',
  styleUrls: ['./custom-confirm-dialog.component.css']
})
export class CustomConfirmDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data:ConfirmDialogData) { }

  ngOnInit(): void {
  }

}
