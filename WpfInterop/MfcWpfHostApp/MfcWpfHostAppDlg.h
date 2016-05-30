
// MfcWpfHostAppDlg.h : header file
//

#pragma once

#include "MyWpfInteropControlProvider.h"


// CMfcWpfHostAppDlg dialog
class CMfcWpfHostAppDlg : public CDialogEx
{
// Construction
public:
	CMfcWpfHostAppDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_MFCWPFHOSTAPP_DIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
private:


protected:
	HICON m_hIcon;
	CMyWpfInteropControlProvider m_MyWpfInteropControlProvider1;
	CMyWpfInteropControlProvider m_MyWpfInteropControlProvider2;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg void OnDestroy();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
};
