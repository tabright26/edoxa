import { connect } from "react-redux";
import { loadStripeBankAccount, updateStripeBankAccount } from "store/root/payment/stripe/bankAccount/actions";
import { UPDATE_STRIPE_BANKACCOUNT_FAIL, StripeBankAccountActions } from "store/root/payment/stripe/bankAccount/types";
import Update from "./Update";
import { compose } from "recompose";
import { injectStripe } from "react-stripe-elements";
import { withtUserProfile } from "store/root/user/container";
import { COUNTRY_CLAIM_TYPE } from "utils/oidc/types";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    updateStripeBankAccount: (data: any) =>
      ownProps.stripe
        .createToken("bank_account", {
          account_holder_name: data.accountHolderName,
          routing_number: data.routingNumber,
          account_number: data.accountNumber,
          currency: data.currency,
          country: ownProps.country
        })
        .then(result => {
          if (result.token) {
            return dispatch(updateStripeBankAccount(result.token.id)).then((action: StripeBankAccountActions) => {
              switch (action.type) {
                case UPDATE_STRIPE_BANKACCOUNT_FAIL: {
                  throwSubmissionError(action.error);
                  break;
                }
              }
            });
          } else {
            return Promise.reject(result.error);
          }
        })
  };
};

const enhance = compose<any, any>(
  injectStripe,
  withtUserProfile(COUNTRY_CLAIM_TYPE),
  connect(
    null,
    mapDispatchToProps
  )
);

export default enhance(Update);
