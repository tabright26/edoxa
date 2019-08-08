import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserStripeBankAccounts } from "../../../../../../store/actions/stripeActions";

const withUserAccountStripeBankAccountContainer = WrappedComponent => {
  class UserAccountStripeBankAccountContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeBankAccounts();
    }

    render() {
      const { hasBankAccount, ...rest } = this.props;
      return <WrappedComponent hasBankAccount={hasBankAccount} {...rest} />;
    }
  }

  const mapStateToProps = state => {
    return {
      hasBankAccount: state.user.account.stripe.bankAccounts.data.length >= 1
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserStripeBankAccounts: () => dispatch(loadUserStripeBankAccounts())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserAccountStripeBankAccountContainer);
};

export default withUserAccountStripeBankAccountContainer;
