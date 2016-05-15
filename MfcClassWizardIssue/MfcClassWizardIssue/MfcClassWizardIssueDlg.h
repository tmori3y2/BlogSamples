
// MfcClassWizardIssueDlg.h : header file
//

#pragma once
#include "UserControl1.h"
#include "UserControl2.h"
#include "UserControl3.h"


// CMfcClassWizardIssueDlg dialog
class CMfcClassWizardIssueDlg : public CDialogEx
{
// Construction
public:
	CMfcClassWizardIssueDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MFCCLASSWIZARDISSUE_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
	CUserControl1 m_objControl1;
	CUserControl2 m_objControl2;
	CUserControl3 m_objControl3;
	CWnd m_objControl4;
	CWnd m_objControl5;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
};
