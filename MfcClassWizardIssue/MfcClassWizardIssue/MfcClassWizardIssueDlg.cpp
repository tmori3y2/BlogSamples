
// MfcClassWizardIssueDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MfcClassWizardIssue.h"
#include "MfcClassWizardIssueDlg.h"
#include "afxdialogex.h"

#import "..\tlb\CSActiveXUserControl3.tlb" no_namespace named_guids

#ifdef _DEBUG
#define new DEBUG_NEW
#include "Debug\csactivexusercontrol3.tlh"
#else
#include "Release\csactivexusercontrol3.tlh"
#endif



// CMfcClassWizardIssueDlg dialog



CMfcClassWizardIssueDlg::CMfcClassWizardIssueDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMfcClassWizardIssueDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMfcClassWizardIssueDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);

	//DDX_Control(pDX, IDC_CUSTOM1, m_objControl1);
	//DDX_Control(pDX, IDC_CUSTOM2, m_objControl2);
	//DDX_Control(pDX, IDC_CUSTOM3, m_objControl3);
}

BEGIN_MESSAGE_MAP(CMfcClassWizardIssueDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CMfcClassWizardIssueDlg message handlers

BOOL CMfcClassWizardIssueDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	m_objControl1.Attach(GetDlgItem(IDC_CUSTOM1)->m_hWnd);
	m_objControl2.Attach(GetDlgItem(IDC_CUSTOM2)->m_hWnd);
	m_objControl3.Attach(GetDlgItem(IDC_CUSTOM3)->m_hWnd);
	m_objControl4.Attach(GetDlgItem(IDC_CUSTOM4)->m_hWnd);
	m_objControl5.Attach(GetDlgItem(IDC_CUSTOM5)->m_hWnd);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMfcClassWizardIssueDlg::OnPaint()
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

		m_objControl1.Invalidate(FALSE);
		//m_objControl2.Refresh();
		m_objControl3.Invalidate(FALSE);
		IOleObjectPtr pOleObj(m_objControl4.GetControlUnknown());
		if (pOleObj != nullptr)
		{
			IUserControl4Ptr iControl4;
			HRESULT hr = pOleObj.QueryInterface(DIID_IUserControl4, &iControl4);
			if (iControl4 != nullptr) iControl4->Refresh();
		}
		m_objControl5.Invalidate(FALSE);
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMfcClassWizardIssueDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

