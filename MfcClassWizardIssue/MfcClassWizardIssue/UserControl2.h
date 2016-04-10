// UserControl2.h  : Declaration of ActiveX Control wrapper class(es) created by Microsoft Visual C++

#pragma once

/////////////////////////////////////////////////////////////////////////////
// CUserControl2

class CUserControl2 : public CWnd
{
protected:
	DECLARE_DYNCREATE(CUserControl2)
public:
	CLSID const& GetClsid()
	{
		static CLSID const clsid
			= { 0xF8335586, 0x7635, 0x4249, { 0x8E, 0xD8, 0xBD, 0x45, 0xA, 0x50, 0x57, 0xE9 } };
		return clsid;
	}
	virtual BOOL Create(LPCTSTR lpszClassName, LPCTSTR lpszWindowName, DWORD dwStyle,
						const RECT& rect, CWnd* pParentWnd, UINT nID, 
						CCreateContext* pContext = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID); 
	}

    BOOL Create(LPCTSTR lpszWindowName, DWORD dwStyle, const RECT& rect, CWnd* pParentWnd, 
				UINT nID, CFile* pPersist = NULL, BOOL bStorage = FALSE,
				BSTR bstrLicKey = NULL)
	{ 
		return CreateControl(GetClsid(), lpszWindowName, dwStyle, rect, pParentWnd, nID,
		pPersist, bStorage, bstrLicKey); 
	}

// Attributes
public:

// Operations
public:

	STDMETHOD(Refresh)()
	{
		HRESULT result;
		InvokeHelper(DISPID_REFRESH, DISPATCH_METHOD, VT_HRESULT, (void*)&result, NULL);
		return result;
	}


};
