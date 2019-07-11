import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTreeModule } from '@angular/material/tree';

import { NotebookRoutingModule } from './notebook-routing.module';
import { NotebookListComponent, DynamicDatabase } from './notebook-list/notebook-list.component';
import { MatIconModule, MatProgressBarModule, MatButtonModule } from '@angular/material';

@NgModule({
  declarations: [NotebookListComponent],
  imports: [
    CommonModule,
    NotebookRoutingModule,
    MatTreeModule,
    MatIconModule,
    MatProgressBarModule,
    MatButtonModule
  ],
  providers:[
    DynamicDatabase
  ]
})
export class NotebookModule { }
