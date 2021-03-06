﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AcBlog.Client.WebAssembly.Interops
{
    public static class LoadingInfoInterop
    {
        public static ValueTask Show(IJSRuntime runtime)
        {
            return runtime.InvokeVoidAsync("acblogInteropLoadingInfoShow");
        }

        public static ValueTask Hide(IJSRuntime runtime)
        {
            return runtime.InvokeVoidAsync("acblogInteropLoadingInfoHide");
        }
    }

    public static class PopoverInterop
    {
        public static ValueTask Enable(IJSRuntime runtime)
        {
            return runtime.InvokeVoidAsync("acblogInteropPopoverEnable");
        }
    }

    public static class TooltipInterop
    {
        public static ValueTask Enable(IJSRuntime runtime)
        {
            return runtime.InvokeVoidAsync("acblogInteropTooltipEnable");
        }
    }

    public static class ModalInterop
    {
        public static ValueTask Show(IJSRuntime runtime, string id)
        {
            return runtime.InvokeVoidAsync("acblogInteropModalAction", id, "show");
        }

        public static ValueTask Hide(IJSRuntime runtime, string id)
        {
            return runtime.InvokeVoidAsync("acblogInteropModalAction", id, "hide");
        }
    }

    public static class ToastInterop
    {
        public static ValueTask Show(IJSRuntime runtime, string id)
        {
            return runtime.InvokeVoidAsync("acblogInteropToastAction", id, "show");
        }

        public static ValueTask Hide(IJSRuntime runtime, string id)
        {
            return runtime.InvokeVoidAsync("acblogInteropToastAction", id, "hide");
        }
    }
}
