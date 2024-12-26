import { Component, OnInit } from '@angular/core';
import { ResumenService } from '../../services/resumen.service';

@Component({
  selector: 'app-resumen',
  templateUrl: './resumen.component.html',
  styleUrls: ['./resumen.component.css']
})
export class ResumenComponent implements OnInit {
  resumen: any; // Aquí se almacenará el resumen recibido

  constructor(private resumenService: ResumenService) {}

  ngOnInit(): void {
    this.cargarResumen();
  }

  cargarResumen(): void {
    this.resumenService.getResumen().subscribe({
      next: (data) => {
        this.resumen = data;
      },
      error: (err) => {
        console.error('Error al cargar el resumen:', err);
      }
    });
  }
}
