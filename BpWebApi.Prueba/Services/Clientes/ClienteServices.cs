using BpAccesoDatos.Prueba;
using BpNegocio.Base;
using BpNegocio.Prueba.Dto.Clientes;
using BpNegocio.Prueba.Entidades.Clientes;
using BpNegocio.Prueba.Entidades.Sujeto;
using Microsoft.EntityFrameworkCore;

namespace BpWebApi.Prueba.Services.Clientes
{
    public class ClienteServices
    {
        private readonly ILogger<ClienteServices> logger;

        public ClienteServices(ILogger<ClienteServices> logger)
        {
            this.logger = logger;
        }

        public DtoResultCliente CrearCliente(PruebaContext contexto, DtoParamCrearCliente parametro)
        {
            DtoResultCliente resultado = new();
            Persona nuevaPersona;
            Cliente nuevoCliente;
            Cliente_Contrasenia contrasenia;
            try
            {
                var genero = contexto.Generos.Where(p => p.Codigo == parametro.CodigoGenero && p.Estado).FirstOrDefault();
                if (genero == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No existe o esta deshabilitado el código de género enviado: {1}", "CCLI001", parametro.CodigoGenero);

                var cliente = contexto.Clientes.Include(o => o.Persona).Where(p => p.Persona.Identificacion == parametro.Identificacion).FirstOrDefault();
                if (cliente != null)
                {
                    if (cliente.Estado != true)
                    {
                        resultado=ActualizarCliente(contexto, parametro);
                    }
                    else {
                        throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada ya existe: {1}", "CCLI002", parametro.Identificacion);
                    }
                }
                else
                {
                    using var transaction = contexto.Database.BeginTransaction();
                    try
                    {
                        nuevaPersona = new Persona()
                        {
                            CodigoGenero = parametro.CodigoGenero.ToUpper(),
                            Identificacion = parametro.Identificacion,
                            Nombre = parametro.Nombre,
                            FechaNacimiento = Convert.ToDateTime(parametro.FechaNacimiento),
                            Direccion = parametro.Direccion,
                            Telefono = parametro.Telefono
                        };
                        contexto.Personas.Add(nuevaPersona);
                        contexto.SaveChanges();

                        nuevoCliente = new Cliente()
                        {
                            PersonaId = nuevaPersona.Id,
                            Fecha = DateTime.Today,
                            Estado = true
                        };
                        contexto.Clientes.Add(nuevoCliente);
                        contexto.SaveChanges();

                        contrasenia = new Cliente_Contrasenia()
                        {
                            ClienteId = nuevoCliente.Id,
                            Contrasenia = parametro.Clave,
                            Estado = true
                        };
                        contexto.Cliente_Contrasenias.Add(contrasenia);
                        contexto.SaveChanges();
                        transaction.Commit();
                        resultado = BuscarCliente(contexto, nuevaPersona.Identificacion, true);
                    }
                    catch (Exception ex)
                    { 
                        transaction.Rollback();
                        logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                        throw;
                    }                
                }
            }
            catch (SistemaExcepcion sex)
            {
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return resultado;
        }

        public DtoResultCliente ActualizarCliente(PruebaContext contexto, DtoParamCrearCliente parametro)
        {
            DtoResultCliente result;
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                var genero = contexto.Generos.Where(p => p.Codigo == parametro.CodigoGenero && p.Estado).FirstOrDefault();
                if (genero == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | No existe o esta deshabilitado el código de género enviado: {1}", "ACLI001", parametro.CodigoGenero);

                var persona = contexto.Personas.Where(p=> p.Identificacion==parametro.Identificacion).FirstOrDefault();
                if (persona == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada no existe: {1}", "ACLI002", parametro.Identificacion);

                persona.CodigoGenero = parametro.CodigoGenero;
                persona.Nombre=parametro.Nombre;
                persona.FechaNacimiento = Convert.ToDateTime(parametro.FechaNacimiento);
                persona.Direccion=parametro.Direccion;
                persona.Telefono=parametro.Telefono;

                var cliente = contexto.Clientes.Where(p => p.PersonaId == persona.Id).FirstOrDefault();
                cliente.Estado = true;

                var cliente_contra = contexto.Cliente_Contrasenias.Where(p => p.ClienteId == cliente.Id).FirstOrDefault();
                cliente_contra.Contrasenia = parametro.Clave;
                cliente_contra.Estado = true;

                contexto.SaveChanges();
                transaction.Commit();
                result = BuscarCliente(contexto, parametro.Identificacion, true);
            }
            catch (SistemaExcepcion sex)
            {
                transaction.Rollback();
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return result;
        }
        public DtoResultCliente DesactivarCliente(PruebaContext contexto, string identificacion)
        {
            DtoResultCliente result;
            using var transaction = contexto.Database.BeginTransaction();
            try
            {
                var cliente = contexto.Clientes.Include(o => o.Persona).Where(p => p.Persona.Identificacion == identificacion).FirstOrDefault();
                if (cliente != null)
                {
                    if (cliente.Estado == true)
                    {
                        cliente.Estado = false;
                        var cliente_contra = contexto.Cliente_Contrasenias.Where(p => p.ClienteId == cliente.Id).FirstOrDefault();
                        cliente_contra.Estado = false;
                        contexto.SaveChanges();
                        transaction.Commit();
                        result = BuscarCliente(contexto, identificacion, false);
                    }
                    else
                    {
                        throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada no existe o esta desactivada: {1}", "DCLI002", identificacion);
                    }
                }
                else
                {
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada no existe o esta desactivada: {1}", "DCLI002", identificacion);
                }
            }
            catch (SistemaExcepcion sex)
            {
                transaction.Rollback();
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return result;
        }
        public DtoResultCliente BuscarCliente(PruebaContext contexto, string identificacion, bool estado)
        {
            DtoResultCliente result;
            try
            {
                var cliente = contexto.Clientes.Include(o => o.Persona).Where(p => p.Persona.Identificacion == identificacion && p.Estado==estado).FirstOrDefault();
                if (cliente == null)
                    throw SistemaExcepcion.CrearException(CodigosMensaje.SERVER_ERROR, "{0} | La identificación ingresada no existe o esta desactivada: {1}", "BCLI001", identificacion);

                result = new DtoResultCliente()
                {
                    Id = cliente.Persona.Id,
                    CodigoGenero = cliente.Persona.CodigoGenero,
                    Identificacion = cliente.Persona.Identificacion,
                    Nombre = cliente.Persona.Nombre,
                    Edad = DateTime.Today.AddTicks(-cliente.Persona.FechaNacimiento.Ticks).Year - 1,
                    Direccion = cliente.Persona.Direccion,
                    Telefono = cliente.Persona.Telefono
                };
            }
            catch (SistemaExcepcion sex)
            {
                logger.LogError(sex, sex.Descripcion, sex.CodigoMensaje.ToString());
                throw sex;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, CodigosMensaje.SERVER_ERROR.ToString());
                throw;
            }
            return result;
        }
    }
}
