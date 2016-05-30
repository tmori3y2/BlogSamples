// Machine generated IDispatch wrapper class(es) created with Add Class from Typelib Wizard

#import "..\\tlb\\MyWpfInteropControls.tlb" no_namespace named_guids
// CMyWpfInteropControlProvider wrapper class

class CMyWpfInteropControlProvider : public COleDispatchDriver
{
public:
	CMyWpfInteropControlProvider() {} // Calls COleDispatchDriver default constructor
	CMyWpfInteropControlProvider(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	CMyWpfInteropControlProvider(const CMyWpfInteropControlProvider& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

	// Attributes
public:

	// Operations
public:


	// IMyWpfInteropControlProvider methods
public:
	void InitializeControl(long region)
	{
		static BYTE parms[] = VTS_I4;
		InvokeHelper(0x100, DISPATCH_METHOD, VT_EMPTY, NULL, parms, region);
	}

	void Dispose()
	{
		InvokeHelper(0x101, DISPATCH_METHOD, VT_EMPTY, NULL, NULL, NULL);
	}

	void Invalidate()
	{
		InvokeHelper(0x102, DISPATCH_METHOD, VT_EMPTY, NULL, NULL, NULL);
	}

	// IMyWpfInteropControlProvider properties
public:

};
