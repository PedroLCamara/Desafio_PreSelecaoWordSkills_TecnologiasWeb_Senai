namespace alatech.Utils
{
    public static class VerificarCompatibilidades
    {
        public static bool TipoSoqueteProcessador_PlacaMae(int IdTipoSoquetePlacaMae, int IdSoqueteProcessador)
        {
            if (IdTipoSoquetePlacaMae != IdSoqueteProcessador)
            {
                return false;
            }
            else return true;
        }
        public static bool TdpProcessador_PlacaMae(int TdpProcessador, int TdpMaximoPlacaMae)
        {
            if (TdpProcessador > TdpMaximoPlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool MemoriaRam_PlacaMae(int IdTpMRamMemoria, int IdTpMRamPlacaMae)
        {
            if (IdTpMRamMemoria != IdTpMRamPlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdMemoriaRam_PlacaMae(int QtdMemoriaRam, int SlotsMRamPlacaMae)
        {
            if (QtdMemoriaRam > SlotsMRamPlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdPlacaVideo_PlacaMae(int QtdPlacaVideo, int SlotsPciPlacaMae)
        {
            if (QtdPlacaVideo > SlotsPciPlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdDspArmzSATA_PlacaMae(int QtdDispositivosArmz, int SlotsSataPlacaMae)
        {
            if (QtdDispositivosArmz > SlotsSataPlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdDspArmzM2_PlacaMae(int QtdDispositivosArmz, int SlotsM2PlacaMae)
        {
            if (QtdDispositivosArmz > SlotsM2PlacaMae)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdDspArmz(int QtdSata, int QtdM2)
        {
            if ((QtdSata + QtdM2) < 0)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdMemoriaRam(int QtdMemoriaRam)
        {
            if (QtdMemoriaRam < 1)
            {
                return false;
            }
            else return true;
        }
        public static bool QtdPlacaVideo(int QtdPlacaVideo)
        {
            if (QtdPlacaVideo < 1)
            {
                return false;
            }
            else return true;
        }
        public static bool MultiplasPlacaVideo(int QtdPlacaVideo, bool SuporteMultiGPU)
        {
            if (QtdPlacaVideo > 1 && SuporteMultiGPU == false)
            {
                return false;
            }
            else return true;
        }
        public static bool PotenciaFonte_PlacasVideo(int QtdPlacasVideo, int PotenciaMinimaPlaca, int PotenciaFonte)
        {
            if ((QtdPlacasVideo * PotenciaMinimaPlaca) > PotenciaFonte)
            {
                return false;
            }
            else return true;
        }
    }
}
