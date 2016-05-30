
// MfcWpfHostAppDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MfcWpfHostApp.h"
#include "MfcWpfHostAppDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#include "Debug\MyWpfInteropControls.tlh"
#else
#include "Release\csactivexusercontrol3.tlh"
#endif


// CMfcWpfHostAppDlg dialog



CMfcWpfHostAppDlg::CMfcWpfHostAppDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_MFCWPFHOSTAPP_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMfcWpfHostAppDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CMfcWpfHostAppDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CLOSE()
	ON_WM_DESTROY()
END_MESSAGE_MAP()


// CMfcWpfHostAppDlg message handlers

BOOL CMfcWpfHostAppDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	CoInitialize(NULL);

	if (m_MyWpfInteropControlProvider1.CreateDispatch(CLSID_MyWpfInteropControlProvider))
	{
		m_MyWpfInteropControlProvider1.InitializeControl((long)GetDlgItem(IDC_STATIC1)->m_hWnd);
	}

	if (m_MyWpfInteropControlProvider2.CreateDispatch(CLSID_MyWpfInteropControlProvider))
	{
		m_MyWpfInteropControlProvider2.InitializeControl((long)GetDlgItem(IDC_STATIC2)->m_hWnd);
	}

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMfcWpfHostAppDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();

		if (m_MyWpfInteropControlProvider1.m_lpDispatch != nullptr)
		{
			m_MyWpfInteropControlProvider1.Invalidate();
		}

		if (m_MyWpfInteropControlProvider2.m_lpDispatch != nullptr)
		{
			m_MyWpfInteropControlProvider2.Invalidate();
		}
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMfcWpfHostAppDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CMfcWpfHostAppDlg::OnDestroy()
{
	if (m_MyWpfInteropControlProvider1.m_lpDispatch != nullptr)
	{
		m_MyWpfInteropControlProvider1.Dispose();
		m_MyWpfInteropControlProvider1.ReleaseDispatch();
	}

	if (m_MyWpfInteropControlProvider2.m_lpDispatch != nullptr)
	{
		m_MyWpfInteropControlProvider2.Dispose();
		m_MyWpfInteropControlProvider2.ReleaseDispatch();
	}

	CDialogEx::OnDestroy();

	// TODO: Add your message handler code here
	CoUninitialize();
}
