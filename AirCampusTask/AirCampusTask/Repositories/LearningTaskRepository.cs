using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AirCampusTask.Models;
using SQLite;

namespace AirCampusTask.Repositories
{
    public class LearningTaskRepository : ILearningTaskRepository
    {
        private SQLiteAsyncConnection _connection;
        
        public event EventHandler<LearningTask> OnLearningTaskAdded;
        public event EventHandler<LearningTask> OnLearningTaskUpdated;

        private async Task CreateConnection()
        {
            if (_connection != null)
            {
                return;
            }

            // todo: Use the special data folder.
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "LearningTasks.db");

            _connection = new SQLiteAsyncConnection(databasePath);
            await _connection.CreateTableAsync<LearningTask>();

            if (await _connection.Table<LearningTask>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new LearningTask()
                {
                    Title = "Welcome to Priming & Staging",
                    Category = "",
                    Instruction =
                        "Work your way through the tasks from top to the bottom. After finishing your task, a few questions will be asked and your tasked is synced to the AirCampus server. If you have no internet connection you can resync tour task by clicking on it. ",
                    Due = DateTime.Now
                });

                List<LearningTask> ttnTasks = new List<LearningTask>();
                ttnTasks.Add(new LearningTask()
                    {Category = "Maak CIS en IV Planning", Title = "Installeer het MWS.", Instruction = ""});
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning",
                    Title = "Installeer de CIS Planner en de IV Planner op het MWS.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.1/5.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning",
                    Title = "Maak een CIS Plan en een IV Plan en initialiseer deze.",
                    Instruction = "Lees de stappen in het document CIS Planner SIM.pdf/IV Planner SIM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Maak CIS en IV Planning", Title = "Voer de Cold Steel Test uit.",
                    Instruction =
                        "Volgens het document TITAAN 4.3 - Cold Steel Test - Server Container C3-A4 en MB10KN (SCE Type 1).pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel", Title = "Installeer UNI op het MWS.",
                    Instruction = "Lees de stappen in het document TITAAN 4.2 SR04 - UNI 2.3 - SIM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel", Title = "Gebruik UNI en het CIS Plan.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel",
                    Title = "Installeer, configureer en controleer de Red Router en de L3-switch.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer het netwerkdeel",
                    Title = "Installeer, configureer en controleer de overige netwerk componenten (SCE en de boxen).",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - UNI - SUM.pdf"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage", Title = "Algemene informatie",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage",
                    Title = "Start de TITAAN Storage Array Configuration Utility (SACU) op het MWS.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage",
                    Title = "Configureer de HP MSA met de TITAAN SACU en het CIS Plan.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Installeer storage", Title = "Installeer de firmware-updates op de HP MSA.",
                    Instruction =
                        "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.3.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Server Priming",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst; 5.4"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Server Priming",
                    Title = "Installeer de updates op de HP ProLiant-server (indien benodigd).",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.4.1.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Server Priming",
                    Title = "Test de hardware van de HP ProLiant-server.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.4.1.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Server Priming",
                    Title = "Reset de ROM Based Setup.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.4.1.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Server Priming",
                    Title = "Configureer de ROM Based Setup",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.4.1.4 of 5.4.1.5"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "ESXi-deployment",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.5"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "ESXi-deployment",
                    Title = "Creeer een opstartbare USB-stick voor de installatie van VMware ESXi.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.5.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "ESXi-deployment",
                    Title = "Installeer VMware ESXi op de host.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.52"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "USI-deploy",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf,. Hfdst 5.6"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "USI-deploy",
                    Title = "Tijdcontrole Red Router - MWS.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.6"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "USI-deploy",
                    Title = "Voer de uitrol van de USI-server uit.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.6.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "USI-deploy",
                    Title = "Configureer de USI-server",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.6.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Controleer de tijd van de USI-server.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Start de TITAAN VM-builder op de USI-server met INFRA.XML",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Start de TITANA s=Startup Manager op de USI-server.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Controleer en monitor met de USI Monitor de voortgang.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Installatie gereed? Start de TITAAN Startup Manager om alle VM's te rebooten.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "VM-builder",
                    Title = "Controleer of de Call Manager een Join Domain heeft gedaan.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.7.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Priming & Staging-accounts",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.8"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Priming & Staging-accounts",
                    Title = "Enable Priming & staging-account (PS account) per CISNode.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.8.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Orchestrator",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.9"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Orchestrator",
                    Title = "Importeer de Orchestrator runbooks.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.9.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Orchestrator",
                    Title = "Start de SC Orchestrator -> CIS Admins runbook (alleen op primaire CISNode).",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.9.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Post-Configuratie SCCM",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.10"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Post-Configuratie SCCM",
                    Title = "Start de Configuration Manager-console.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.10.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Post-Configuratie SCCM",
                    Title = "Configureer de default client settings",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.10.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Post-Configuratie SCCM",
                    Title = "Creeer de applicaties in Configuration Manager.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.10.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Client",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.11 en 5.11.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Client",
                    Title = "Configureer MDT.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.11.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Client",
                    Title = "Voeg het MWS toe aan het domein.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.11.6"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Verwidjer het CIS Plan met de naam INFRA.xml",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Ontkoppel de USI-cd images",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Verwijder de USI-cd ISO uit de datastore.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Maak een back-up van de USI-server.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6.4.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Afronding installatie",
                    Title = "Verwijder de USI-server.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 6.4.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Voer de staging van TCTS uit.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Voer de Unit provisioning (OC) uit.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Voer de Unit provisioning (Eenheden) uit.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Verwijder de Merkingen en Rubriceringen Mail CLient.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Rond provisioning af.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.4"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "Werk EnPoint protection definition-updates bij.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 7.5"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Provisioning",
                    Title = "TITAAN Security Updates",
                    Instruction = "TITAAN-disk:\\\\TTN43\\\\Security Update"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Client Deployment",
                    Title = "Deploy clients.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.11.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Client Deployment",
                    Title = "Encrypt clients met verwijderbare harddisks(s) met BitLocker (optioneel).",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 5.11.4 en 5.11.5"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Indicatoren en aansluitingen NAS.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8.1"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Configureer de back-up storage.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8.2"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Installeer Acronis Backup.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8.3"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Configureer de back-ups.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8.4"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "Back-Up",
                    Title = "Configureer de replicatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 8.5"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "UPS configureren",
                    Title = "Algemene informatie.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 9"
                });
                ttnTasks.Add(new LearningTask()
                {
                    Category = "UPS configureren",
                    Title = "Configureer een enkelvoudige UPS.",
                    Instruction = "Lees de stappen in het document TITAAN 4.3 - Platforminstallatie - SIM.pdf, Hfdst 9.1"
                });
                await _connection.InsertAllAsync(ttnTasks);
            }
            

        }
        
        public async Task<List<LearningTask>> GetLearningTasks()
        {
            await CreateConnection();
            return await _connection.Table<LearningTask>().ToListAsync();
        }

        public async Task AddLearningTask(LearningTask learningTask)
        {
            await CreateConnection();
            await _connection.InsertAsync(learningTask);
            OnLearningTaskAdded?.Invoke(this, learningTask);
        }

        public async Task UpdateLearningTask(LearningTask learningTask)
        {
            await CreateConnection();
            await _connection.UpdateAsync(learningTask);
            OnLearningTaskUpdated?.Invoke(this, learningTask);
        }

        public async Task AddOrUpdate(LearningTask learningTask)
        {
            if (learningTask.Id == 0)
            {
                await AddLearningTask(learningTask);
            }
            else
            {
                await UpdateLearningTask(learningTask);
            }
        }
        
    }
}