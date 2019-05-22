namespace Computer_Era_X.DataTypes.Enums
{
    public enum ItemTypes
    {
        Case,
        Motherboard,
        RAM,
        PSU,
        CPU,
        CPUCooler,
        HDD,
        Monitor,
        VideoCard,
        OpticalDrive,
        Mouse,
        Keyboard,
        OpticalDisc,
        OS,
        Program,
    }
    public enum Sockets
    {
        AM4,
        LGA1151,
        LGA775,
        LGA1150,
        LGA1155,
        LGA1156,
        AM2,
        AM2Plus,
        AM3,
        AM3Plus,
        FM1,
        FM2,
        FM2Plus,
        LGA1366,
        Socket754,
        Socket939,
        Socket940,
    }
    public enum CaseTypes
    {
        AT,
        ATX,
        Slim,
        MiniTower,
        MidiTower,
        BigTower,
        Barebone,
        Rack,
        TinyTower,
    }
    public enum MotherboardTypes
    {
        ATX,
        MiniATX,
        NLX,
        FlexATX,
        MicroATX,
        CEB,
        WTX,
        PicoITX,
        NanoITX,
        MiniITX,
        PicoBTX,
        MicroBTX,
        BTX,
        LPX,
        BabyAT,
        EATX,
    }
    public enum PSUTypes
    {
        ATX
    }
    public enum HDDFormFactor
    {
        ThreeFive,
        TwoFive
    }

    public enum HDDInterface
    {
        // ReSharper disable once InconsistentNaming
        Sata2_0,
        // ReSharper disable once InconsistentNaming
        Sata3_0,
        IDE
    }
    public enum MotherboardBIOS
    {
        AMI,
        Award,
    }

    public enum RAMTypes
    {
        SIMM,
        FPM,
        DIMM,
        RIMM,
        DDR,
        DDR2,
        DDR3,
        DDR4
    }
    public enum VideoInterface
    {
        VGA,
        DVI,
        HDMI,
        DisplayPort,
        DVI_D,
        MiniHDMI,
    }

    public enum TypeConnectorMotherboard
    {
        pin20plus4
    }

    public enum FileSystem
    {
        FAT12,
        FAT16,
        FAT32,
        VFAT,
        NTFS
    }

    public enum Interface
    {
        PCI_E16x3_0,
        PCI_E16x2_0,
        PCI_E16,
    }

    public enum TypeVideoMemory
    {
        GDDR5,
        GDDR4,
        GDDR3,
        GDDR2,
        GDDR,
    }

    public enum OpticalDriveInterface
    {
        IDE,
        SATA
    }
    public enum OpticalDiscType
    {
        CD,
        DVD
    }
    public enum InputInterfaces
    {
        PSby2,
        USB,
    }
}
