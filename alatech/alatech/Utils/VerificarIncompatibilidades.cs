namespace alatech.Utils
{
    /// <summary>
    /// Verifica diferentes casos de possíveis incompatibilidades em uma máquina
    /// </summary>
    public static class VerificarIncompatibilidades
    {
        public static string TipoSoqueteProcessador_PlacaMae(int IdTipoSoquetePlacaMae, int IdSoqueteProcessador)
        {
            if (IdTipoSoquetePlacaMae != IdSoqueteProcessador)
            {
                return $"Processador e Placa Mãe-O tipo de soquete da placa mãe (Id tipo:{IdTipoSoquetePlacaMae}) é incompatível com o do processador (Id tipo:{IdSoqueteProcessador})";
            }
            else return null;
        }
        public static string TdpProcessador_PlacaMae(int TdpProcessador, int TdpMaximoPlacaMae)
        {
            if (TdpProcessador > TdpMaximoPlacaMae)
            {
                return $"Processador e Placa Mãe-O tdp maximo da placa mãe ({TdpMaximoPlacaMae}) é incompatível com o do processador ({TdpProcessador})";
            }
            else return null;
        }
        public static string MemoriaRam_PlacaMae(int IdTpMRamMemoria, int IdTpMRamPlacaMae)
        {
            if (IdTpMRamMemoria != IdTpMRamPlacaMae)
            {
                return $"Memória RAM e Placa Mãe-O tipo de memoria RAM da placa mãe (Id tipo: {IdTpMRamPlacaMae}) é incompatível com o da memória RAM selecionada (Id tipo: {IdTpMRamMemoria})";
            }
            else return null;
        }
        public static string QtdMemoriaRam_PlacaMae(int QtdMemoriaRam, int SlotsMRamPlacaMae)
        {
            if (QtdMemoriaRam > SlotsMRamPlacaMae)
            {
                return $"Memória RAM e Placa Mãe-A quantidade de slots de memórias RAM da placa mãe ({SlotsMRamPlacaMae}) é incompatível com a quantidade de memórias RAM especificada({QtdMemoriaRam})";
            }
            else return null;
        }
        public static string QtdPlacaVideo_PlacaMae(int QtdPlacaVideo, int SlotsPciPlacaMae)
        {
            if (QtdPlacaVideo > SlotsPciPlacaMae)
            {
                return $"Placa de vídeo e Placa Mãe-A quantidade de slots Pci da placa mãe ({SlotsPciPlacaMae}) é incompatível com a quantidade de placas de vídeo especificada({QtdPlacaVideo})";
            }
            else return null;
        }
        public static string QtdDspArmzSATA_PlacaMae(int QtdDispositivosArmz, int SlotsSataPlacaMae)
        {
            if (QtdDispositivosArmz > SlotsSataPlacaMae)
            {
                return $"Dispositivos de armazenamento SATA e Placa Mãe-A quantidade de slots SATA da placa mãe ({SlotsSataPlacaMae}) é incompatível com a quantidade de dispositivos de armazenamento SATA especificada({QtdDispositivosArmz})";
            }
            else return null;
        }
        public static string QtdDspArmzM2_PlacaMae(int QtdDispositivosArmz, int SlotsM2PlacaMae)
        {
            if (QtdDispositivosArmz > SlotsM2PlacaMae)
            {
                return $"Dispositivos de armazenamento M2 e Placa Mãe-A quantidade de slots M2 da placa mãe ({SlotsM2PlacaMae}) é incompatível com a quantidade de dispositivos de armazenamento M2 especificada({QtdDispositivosArmz})";
            }
            else return null;
        }
        public static string QtdDspArmz(int QtdSata, int QtdM2)
        {
            if ((QtdSata + QtdM2) < 1)
            {
                return $"Dispositivo de armazenamento-A quantidade de dispositivos de armazenamento deve ter um total maior que zero";
            }
            else return null;
        }
        public static string QtdMemoriaRam(int QtdMemoriaRam)
        {
            if (QtdMemoriaRam < 1)
            {
                return $"Memória RAM-A quantidade de memórias RAM deve ter um total maior que zero";
            }
            else return null;
        }
        public static string QtdPlacaVideo(int QtdPlacaVideo)
        {
            if (QtdPlacaVideo < 1)
            {
                return $"Placa de vídeo-A quantidade de placas de vídeo deve ter um total maior que zero";
            }
            else return null;
        }
        public static string MultiplasPlacaVideo(int QtdPlacaVideo, bool SuporteMultiGPU)
        {
            if (QtdPlacaVideo > 1 && SuporteMultiGPU == false)
            {
                return $"Placas de vídeo-O modelo de placa de vídeo especificado não possui suporte a Multiplas placas de vídeo";
            }
            else return null;
        }
        public static string PotenciaFonte_PlacasVideo(int QtdPlacasVideo, int PotenciaMinimaPlaca, int PotenciaFonte)
        {
            if ((QtdPlacasVideo * PotenciaMinimaPlaca) > PotenciaFonte)
            {
                return $"Placas de vídeo e Fonte-O modelo de fonte especificado possui uma potência ({PotenciaFonte}) menor do que a potência mínima total das placas de vídeo ({QtdPlacasVideo * PotenciaMinimaPlaca})";
            }
            else return null;
        }
    }
}
