namespace Retro.Front.Components.Common.Services;

public class OverlayService
{
    private int _loadingCount;
    
    public bool IsLoading => _loadingCount > 0;
    
    public void StartLoading()
    {
        _loadingCount++;
    }
    
    public void StopLoading()
    {
        _loadingCount--;
        if (_loadingCount < 0)
        {
            _loadingCount = 0;
        }
    }
}