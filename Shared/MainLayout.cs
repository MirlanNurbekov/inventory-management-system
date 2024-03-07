@inherits LayoutComponentBase
@using CinemaGo.DataModels.CustomModels;

@using CinemaGo.Web.Services;

@inject IUserPanelService userPanelService;

@inject ProtectedSessionStorage sessionStorage;

@inject NavigationManager navManager;

<style>
    .mainlayout {
        max-width: 100%;
        margin: auto;
        padding: 10px;
        background: white;
    }

    * {
        box-sizing: border-box;
    }

    .header {
        color: black;
        padding: 5px;
        border: 1px groove #808080;
        border-radius: 7px;
        background: linear-gradient(-45deg, #ffd800, #808080, #23a6d5, #f5f542);
        background-size: 400% 400%;
    }

    .footer {
        color: black;
        padding: 5px;
        border: 1px groove #808080;
        border-radius: 7px;
        background: linear-gradient(-45deg, #ffd800, #808080, #23a6d5, #f5f542);
        background-size: 400% 400%;
    }

    .col-container {
        display: table;
        width: 100%;
        border-radius: 25px;
    }

    .col {
        display: table-cell;
        padding: 16px;
    }

    .divleft {
        float: left;
    }

    .divright {
        float: right;
    }
</style>

<div class="mainlayout">
    <div style="padding-bottom:25px;">
        <div class="divleft" style="padding-right: 15px;">
            <NavLink href="/">
                Home
            </NavLink>
        </div>
        <div class="divleft">
            @if (IsUserLoggedIn)
            {
                <i class="fa fa-user"></i><span>Welcome, @LoggedInUserName</span>
            }
            else
            {
                <i class="fa fa-user"></i><span>Welcome, </span>
            }
        </div>
        <div class="divright">
            @if (IsUserLoggedIn)
            {
                <div style="padding-left:15px;">
                    <i class="fa fa-sign-out"></i> <a @onclick="@Logout" href="#">Logout</a>
                </div>
            }

        </div>
        <div class="divright" style="padding-left:15px">
            @if (IsUserLoggedIn)
            {
                <NavLink href="myaccount">
                    MyAccount
                </NavLink>
            }
        </div>
        <div class="divright" style="padding-left:15px;">
            @if (!IsUserLoggedIn)
            {
                <NavLink href="login">
                    Login
                </NavLink>
            }
        </div>
        <div class="divright" style="padding-left:15px;">
            @if (!IsUserLoggedIn)
            {
                <NavLink href="register">
                    Register
                </NavLink>
            }
        </div>
    </div>
    <NavbarUser />
    
    @*<div class="header">
        <div class="col-container">
            <div class="col" style="border-radius: 25px;">
                <NavLink href="/">
                    <img src="images/cinemaLogo.png" style="cursor: pointer;" />
                </NavLink>
            </div>
            <div class="col">
                <a href="/mycart">
                    <i class="fa fa-shopping-cart" style="font-size:48px;color:black">
                        <span style="font-size:35px; vertical-align:top; padding:7px; color:black; border-radius:50%; padding-top:10px;font-weight:bold;"><sup>@cartCount</sup></span>
                    </i>
                </a>
            </div>
        </div>
    </div>*@
    <div>
        <CascadingValue Value="event_notify">
            @Body
        </CascadingValue>
    </div>
    <div class="footer">
        &copy; 2023- Movie Cart
    </div>
</div>

@code{
    public int cartCount = 0;
    public int layoutState = 0;
    public bool IsUserLoggedIn = false;
    public string LoggedInUserName = "User";
    public List<CartModel> myCart { get; set; }

    EventCallback event_notify => EventCallback.Factory.Create(this, NotifyLayout);

    private async Task NotifyLayout()
    {
        IsUserLoggedIn = await userPanelService.IsUserLoggedIn();

        if (IsUserLoggedIn && layoutState == 0)
        {
            var userNameSession = await sessionStorage.GetAsync<string>("userName");
            LoggedInUserName = userNameSession.Value;

            var checkoutSession = await sessionStorage.GetAsync<string>("checkoutAlert");

            if (checkoutSession.Success)
            {
                await sessionStorage.DeleteAsync("checkoutAlert");
                navManager.NavigateTo("/mycart");
            }

            StateHasChanged();
            layoutState++;
        }
        if (layoutState == -1)
        {
            StateHasChanged();
        }
        var result = await sessionStorage.GetAsync<List<CartModel>>("myCart");

        if (result.Success)
        {
            myCart = result.Value;
            cartCount= myCart.Count;
            await sessionStorage.DeleteAsync("updateCartFlag");
            StateHasChanged();
        }
        else
        {
            cartCount = 0;
        }


    }


    private async void Logout()
    {
        await sessionStorage.DeleteAsync("userKey");
        await sessionStorage.DeleteAsync("userName"); 
        await sessionStorage.DeleteAsync("userEmail");
        await NotifyLayout();
        navManager.NavigateTo("/"); 
        layoutState = -1;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await NotifyLayout();
        }
    }
}