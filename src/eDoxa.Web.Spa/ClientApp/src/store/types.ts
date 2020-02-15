import { reducer as rootReducer } from "store/reducer";
import {
  LoadStripeBankAccountAction,
  UpdateStripeBankAccountAction,
  LoadStripePaymentMethodsAction,
  AttachStripePaymentMethodAction,
  DetachStripePaymentMethodAction,
  UpdateStripePaymentMethodAction,
  LoadStripeCustomerAction,
  UpdateStripeCustomerDefaultPaymentMethodAction,
  LoadStripeAccountAction
} from "./actions/payment/types";
import {
  LoadUserTransactionHistoryAction,
  DepositTransactionAction,
  RedeemPromotionAction,
  WithdrawTransactionAction
} from "./actions/cashier/types";
import {
  LoadChallengesAction,
  LoadChallengeAction,
  LoadChallengeHistoryAction,
  RegisterChallengeParticipantAction
} from "./actions/challenge/types";
import {
  LoadUserPhoneAction,
  UpdateUserPhoneAction,
  ForgotUserPasswordAction,
  ResetUserPasswordAction,
  LoadUserProfileAction,
  CreateUserProfileAction,
  UpdateUserProfileAction,
  LoadUserEmailAction,
  ConfirmUserEmailAction,
  LoadUserDoxatagHistoryAction,
  ChangeUserDoxatagAction,
  LoadUserAddressBookAction,
  CreateUserAddressAction,
  UpdateUserAddressAction,
  DeleteUserAddressAction,
  RegisterUserAccountAction,
  LoginUserAccountAction,
  LogoutUserAccountAction,
  ResendUserEmailActionCreator,
  ResendUserEmailAction
} from "./actions/identity/types";
import {
  UnlinkGameCredentialAction,
  ValidateGameAuthenticationAction,
  GenerateGameAuthenticationAction
} from "./actions/game/types";
import {
  LoadClanMembersAction,
  KickClanMemberAction,
  DownloadClanLogoAction,
  UploadClanLogoAction,
  LoadClansAction,
  LoadClanAction,
  CreateClanAction,
  LeaveClanAction,
  LoadClanInvitationsAction,
  LoadClanInvitationAction,
  SendClanInvitationAction,
  AcceptClanInvitationAction,
  DeclineClanInvitationAction
} from "./actions/clan/types";

export type RootState = ReturnType<typeof rootReducer>;

export type RootActions =
  | LoadStripeBankAccountAction
  | UpdateStripeBankAccountAction
  | LoadUserTransactionHistoryAction
  | DepositTransactionAction
  | WithdrawTransactionAction
  | RedeemPromotionAction
  | LoadChallengesAction
  | LoadChallengeAction
  | LoadChallengeHistoryAction
  | RegisterChallengeParticipantAction
  | LoadStripePaymentMethodsAction
  | AttachStripePaymentMethodAction
  | DetachStripePaymentMethodAction
  | UpdateStripePaymentMethodAction
  | LoadStripeCustomerAction
  | UpdateStripeCustomerDefaultPaymentMethodAction
  | LoadStripeAccountAction
  | LoadUserPhoneAction
  | UpdateUserPhoneAction
  | ForgotUserPasswordAction
  | ResetUserPasswordAction
  | LoadUserProfileAction
  | CreateUserProfileAction
  | UpdateUserProfileAction
  | LoadUserEmailAction
  | ConfirmUserEmailAction
  | LoadUserDoxatagHistoryAction
  | ChangeUserDoxatagAction
  | LoadUserAddressBookAction
  | CreateUserAddressAction
  | UpdateUserAddressAction
  | DeleteUserAddressAction
  | RegisterUserAccountAction
  | LoginUserAccountAction
  | LogoutUserAccountAction
  | UnlinkGameCredentialAction
  | ValidateGameAuthenticationAction
  | GenerateGameAuthenticationAction
  | LoadClanMembersAction
  | KickClanMemberAction
  | DownloadClanLogoAction
  | UploadClanLogoAction
  | LoadClansAction
  | LoadClanAction
  | CreateClanAction
  | LeaveClanAction
  | LoadClanInvitationsAction
  | LoadClanInvitationAction
  | SendClanInvitationAction
  | AcceptClanInvitationAction
  | DeclineClanInvitationAction
  | ResendUserEmailAction;
