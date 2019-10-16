import { connect } from "react-redux";
import { loadStripeBankAccount, updateStripeBankAccount } from "store/root/payment/stripe/bankAccount/actions";
import Update from "./Update";
import { compose } from "recompose";
import { injectStripe } from "react-stripe-elements";
import { withtUserProfile } from "store/root/user/container";

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
            console.log(result.token);
            return dispatch(updateStripeBankAccount(result.token.id)).then(() => dispatch(loadStripeBankAccount()));
          } else {
            return Promise.reject(result.error);
          }
        })
  };
};

const enhance = compose<any, any>(
  injectStripe,
  withtUserProfile("country"),
  connect(
    null,
    mapDispatchToProps
  )
);

export default enhance(Update);
