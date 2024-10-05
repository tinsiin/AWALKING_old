Module MV

    '/////////////////////////////////////////////////
    '//                                             //
    '//    MusicVillage Definition basic modules    //
    '//                    2004 (C)DreamROM         //
    '//                       Last Update:04/02/28  //
    '//                                             //
    '/////////////////////////////////////////////////
    '
    '

    '  Template of the type of function which is used in callback.
    '  Public Function lpfnCalback(ByVal dwMsg As Long, ByVal dwParam As Long, ByVal dwUser As Long) As Long
    '

    '  Type of the handle
    '  HMVSOUND - Long

    '  Version of MusicVillage - You had better check the version of 
    '  DLL's and them when you load MVillage.DLL using MVGetVersion function.

    '  MusicVillage constant values
    '
    Public Const MV_SYSEX_GM = 0
    Public Const MV_SYSEX_GS = -1
    Public Const MV_SYSEX_XG = -2
    Public Const MV_NO_SINGLE = -1
    Public Const MV_MAX_STRING = 128
    Public Const MV_INVALID_DEVICE_ID = -2
    Public Const MV_MIN_TEMPO = 0.25
    Public Const MV_MAX_TEMPO = 2.0#
    Public Const MV_MIN_INSTRUMENT = 0
    Public Const MV_MAX_INSTRUMENT = 127
    Public Const MV_MIN_FADE_TIME = 1000
    Public Const MV_MAX_FADE_TIME = 10000
    Public Const MV_DEFAULT_TIMEOUT = 100
    Public Const MV_MIN_TIMEOUT = 100
    Public Const MV_MAX_TIMEOUT = 1000
    Public Const MV_MIN_VOLUME = 0
    Public Const MV_MAX_VOLUME = 100

    '  MusicVillage constant error values
    Public Const MVERR_NOERROR = 0
    Public Const MVERR_SUCCESS = MVERR_NOERROR
    Public Const MVERR_RETURNOK = MVERR_NOERROR
    Public Const MVERR_LACKMEMORY = 10
    Public Const MVERR_SYSTEMERROR = 11
    Public Const MVERR_INITIALIZE = 12
    Public Const MVERR_RETURNCANCEL = 13
    Public Const MVERR_LOAD_STRING = 14
    Public Const MVERR_FAILED_SET_TIMER = 19
    Public Const MVERR_FILE_OPEN = 20
    Public Const MVERR_FILE_INVALID_FORMAT = 21
    Public Const MVERR_FILE_NOT_PCM = 22
    Public Const MVERR_UNSUPPORTED_FORMAT = 29
    Public Const MVERR_DEVICE_OPENED = 30
    Public Const MVERR_DEVICE_CLOSED = 31
    Public Const MVERR_DEVICE_NO_ENTRY = 32
    Public Const MVERR_DEVICE_INVALID_NUMBER = 33
    Public Const MVERR_DEVICE_CANNOT_OPEN = 34
    Public Const MVERR_WAVE_DEVICE_NO_ENTRY = 35
    Public Const MVERR_NOW_PLAYING = 40
    Public Const MVERR_NOT_PLAYING = 41
    Public Const MVERR_TIMEOUT = 42
    Public Const MVERR_INVALID_COMMAND = 43
    Public Const MVERR_INVALID_STATE = 44
    Public Const MVERR_INVALID_HANDLE = 45
    Public Const MVERR_INVALID_PARAMETER = 46
    Public Const MVERR_INVALID_CAPS = 47
    Public Const MVERR_NOBUFFERONMEMORY = 48
    Public Const MVERR_INVALID_CALL_INTERVAL = 49
    Public Const MVERR_NOW_FADINGOUT = 50
    Public Const MVERR_NOW_FADINGIN = 51
    Public Const MVERR_FILE_TOO_LARGE = 52

    '  Type of MusicVillage capabilities.
    '  typedef enum MVCAPSTYPE{
    Public Const MVCAPS_ACTIONFLAG = 0
    Public Const MVCAPS_CALLBACKPTR = 1
    Public Const MVCAPS_USERPARAM = 2
    Public Const MVCAPS_TEMPO = 3
    Public Const MVCAPS_FADEOUT = 4
    Public Const MVCAPS_FADEIN = 5
    Public Const MVCAPS_SINGLE = 6
    Public Const MVCAPS_TIMEOUT = 7
    Public Const MVCAPS_SHORTMSG = 8
    Public Const MVCAPS_VOLUME = 9
    '  };

    '  Callback messages.
    Public Const MVMSG_DEVICE_OPENERROR = 255
    Public Const MVMSG_DEVICE_OPEN = 0
    Public Const MVMSG_DEVICE_CLOSE = 1
    Public Const MVMSG_LOAD_FILE = 2
    Public Const MVMSG_PLAY = 3
    Public Const MVMSG_STOP = 4
    Public Const MVMSG_WAIT = 5
    Public Const MVMSG_RESTART = 6
    Public Const MVMSG_LOOP = 7
    Public Const MVMSG_FADEOUT = 8
    Public Const MVMSG_FADEIN = 9

    '  Constant values of the state in MusicVillage
    Public Const MVS_EMPTY = -1
    Public Const MVS_NOMSG = 0
    Public Const MVS_PLAYING = 1
    Public Const MVS_WAIT = 2
    Public Const MVS_STOP = 3

    '  Flags for MVCAPS_ACTIONFLAG
    Public Const MVAF_GMRESET = 1
    Public Const MVAF_GSRESET = 2
    Public Const MVAF_XGRESET = 4
    Public Const MVAF_NOSENDSYSEX = 16

    '  Flags for CDDLG struct in dw Flags member
    Public Const CDDF_HWNDPARENT = 1
    Public Const CDDF_DEVICENAME = 2
    Public Const CDDF_HOOKPROC = 4
    Public Const CDDF_RESERVED = 256
    Public Const CDDF_INITIALIZE_DEVICE = 512
    Public Const CDDF_HIDEMIDIMAPPER = 1024
    Public Const CDDF_ACTIVEUPDATE = 2048

    '  Playback flags for Wave sound.
    Public Const PBO_PLAYBACK = &H80000000 '    Public Const PBO_PLAYBACK = & 80000000h
    Public Const PBO_USE_LOOP_BEGIN = 1
    Public Const PBO_USE_LOOP_END = 2
    Public Const PBO_LOOP = 4

    '  ////////        Structs         ////////
    '

    '  MusicVillage StatusInfo struct.
    Public Structure MVStatusInfo
        Public dwSize As Long
        Public nUsedDevice As Long
        Public nState As Long
        Public dwCurPos As Long
        Public dwMusicLength As Long
        Public fTempo As Single
        Public lpfnProc As Long
        Public dwUser As Long
    End Structure  '  MVStatusInfo

    '  MusicVillage CDDLG struct
    Public Structure ChooseDeviceDlg
        Public dwSize As Long
        Public dwFlags As Long
        Public hWndParent As Long
        Public nDevice As Long
        Public lpszDeviceName As String
        Public lpfnHookProc As Long
    End Structure        '  ChooseDeviceDlg

    '  ////////    Exported function    ////////
    '

    Public Declare Function MusicVillageInitialize Lib "MVillage.dll" ()
    Public Declare Function MVOpenDevice Lib "MVillage.dll" (ByVal nDev As Long, ByVal hEvent As Long) As Long
    Public Declare Function MVCloseDevice Lib "MVillage.dll" () As Long
    Public Declare Function MVLoadMusic Lib "MVillage.dll" (ByVal lpFileName As String) As Long
    Public Declare Function MVPlayMusic Lib "MVillage.dll" () As Long
    Public Declare Function MVStopMusic Lib "MVillage.dll" () As Long
    Public Declare Function MVWaitMusic Lib "MVillage.dll" () As Long
    Public Declare Function MVRestartMusic Lib "MVillage.dll" () As Long
    Public Declare Function MVGetStatus Lib "MVillage.dll" (ByRef lpmvsi As MVStatusInfo) As Long
    Public Declare Function MVSendSysEx Lib "MVillage.dll" (ByVal nLength As Long, ByRef lpData As Byte) As Long

    Public Declare Function MVSeek Lib "MVillage.dll" (dwPos As Long) As Long
    Public Declare Function MVSetCaps Lib "MVillage.dll" (ByVal MVCaps As Long, ByVal dwParam As Long) As Long
    Public Declare Function MVGetCaps Lib "MVillage.dll" (ByVal MVCaps As Long, ByRef lpParam As Long) As Long
    Public Declare Function MVGetVersion Lib "MVillage.dll" (ByRef lpVersion As Long) As Long
    Public Declare Function MVGetIcon Lib "MVillage.dll" (ByRef lpIcon As Long) As Long
    Public Declare Function MVGetErrorString Lib "MVillage.dll" (ByVal dwError As Long, ByVal lpReturnString As String, ByVal dwMaxLength As Long) As Long
    Public Declare Function MVChooseDeviceDialog Lib "MVillage.dll" (ByVal lpcdd As ChooseDeviceDlg) As Long
    Public Declare Function MVGetBufferPtr Lib "MVillage.dll" (ByVal dwFlags As Long, ByRef ppBuffer As Long, ByRef lpLength As Long) As Long

    Public Declare Function MVOpenSound Lib "MVillage.dll" (ByVal lpMVHandle As Long, ByVal nDev As Long, ByVal lpFileName As String) As Long
    Public Declare Function MVCloseSound Lib "MVillage.dll" (ByVal hMVSound As Long) As Long
    Public Declare Function MVPlaySound Lib "MVillage.dll" (ByVal hMVSound As Long, ByVal dwOption As Long) As Long
    Public Declare Function MVStopSound Lib "MVillage.dll" (ByVal hMVSound As Long) As Long
    Public Declare Function MVWaitSound Lib "MVillage.dll" (ByVal hMVSound As Long) As Long
    Public Declare Function MVRestartSound Lib "MVillage.dll" (ByVal hMVSound As Long) As Long

    '
    '  End of the definitions
    '
    '



End Module
