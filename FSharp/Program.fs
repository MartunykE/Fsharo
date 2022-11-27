// Types
type UserSubscriptionInfo() = 
    member val BasicSearchesAmount = 0 with get, set
    member val VehicleSearchesAmount = 0 with get, set
    member val TokensAmount = 0 with get, set

type TokensInfo() = 
    member val TokensAmount = 0 with get, set

type SubscriptionPlan() = 
    member val BasicSearchesAmount = 0 with get, set
    member val VehicleSearchesAmount = 0 with get, set

type Choice<'a, 'b> =  | Choice1Of2 of 'a | Choice2Of2 of 'b


//Functions
let getProductInfo (productId: string) =
    if productId = "level 1" then
        Choice1Of2 (SubscriptionPlan(BasicSearchesAmount = 5, VehicleSearchesAmount = 5 ))
    else 
        Choice2Of2 (TokensInfo(TokensAmount = 10))

let procesSubscriptionUpdate (userSubscription: UserSubscriptionInfo, subscriptionPlan: SubscriptionPlan) =
    let updatedSubscription = UserSubscriptionInfo();
    updatedSubscription.BasicSearchesAmount <- userSubscription.BasicSearchesAmount + subscriptionPlan.BasicSearchesAmount;
    updatedSubscription.VehicleSearchesAmount <- userSubscription.VehicleSearchesAmount + subscriptionPlan.VehicleSearchesAmount;
    updatedSubscription.TokensAmount <- userSubscription.TokensAmount;

    updatedSubscription

let procesTokensUpdate (userSubscription: UserSubscriptionInfo, tokensInfo: TokensInfo) =
    let updatedSubscription = UserSubscriptionInfo();
    updatedSubscription.BasicSearchesAmount <- userSubscription.BasicSearchesAmount;
    updatedSubscription.VehicleSearchesAmount <- userSubscription.VehicleSearchesAmount;
    updatedSubscription.TokensAmount <- userSubscription.TokensAmount + tokensInfo.TokensAmount;

    updatedSubscription


let buyProduct(productId : string, userSubscription: UserSubscriptionInfo) = 
    let productInfo =  getProductInfo productId;

    let updatedSubscription = 
        match productInfo with
        | Choice1Of2 subscriptionInfo -> procesSubscriptionUpdate(userSubscription, subscriptionInfo) 
        | Choice2Of2 tokensInfo -> procesTokensUpdate(userSubscription, tokensInfo);

    updatedSubscription

    

//Application
let userSubscription = (UserSubscriptionInfo(BasicSearchesAmount = 0 , VehicleSearchesAmount = 0, TokensAmount = 0));

let subscriptionProductId = "level 1";

let updatedWithSubscription = buyProduct(subscriptionProductId, userSubscription)

let updatedWithTokens = buyProduct("", updatedWithSubscription)

printfn($"basic = { updatedWithSubscription.BasicSearchesAmount } , vehicles = { updatedWithSubscription.VehicleSearchesAmount}, tokens = {updatedWithSubscription.TokensAmount}");
printfn($"basic = { updatedWithTokens.BasicSearchesAmount } , vehicles = { updatedWithTokens.VehicleSearchesAmount}, tokens = {updatedWithTokens.TokensAmount}");


// Output 
//basic = 5 , vehicles = 5, tokens = 0
//basic = 5 , vehicles = 5, tokens = 10


