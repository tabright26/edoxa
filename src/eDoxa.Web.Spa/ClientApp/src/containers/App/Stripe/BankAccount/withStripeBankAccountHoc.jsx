import React, { Component } from "react";
import { connect } from "react-redux";

import { loadUserStripeBankAccounts } from "../../../../store/actions/stripeActions";

const withStripeBankAccountHoc = WrappedComponent => {
  class StripeBankAccountContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeBankAccounts();
    }

    render() {
      const { hasBankAccount, ...attributes } = this.props;
      return <WrappedComponent hasBankAccount={hasBankAccount} {...attributes} />;
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
  )(StripeBankAccountContainer);
};

export default withStripeBankAccountHoc;
