using System;

public interface IAccesoADatos<T>
{
    List<T> Obtener();
    void Guardar(List<T> datos);
    bool Eliminar(int id);
    
}