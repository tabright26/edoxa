import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalanceForToken } from "../../../../../../store/actions/cashierActions";

const withUserAccountBalanceTokenHoc = WrappedComponent => {
  class UserAccountBalanceTokenContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountBalanceForToken();
    }

    render() {
      return <WrappedComponent currency={this.props.token} />;
    }
  }

  const mapStateToProps = state => {
    return {
      token: state.user.account.balance.token
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountBalanceForToken: () => dispatch(loadUserAccountBalanceForToken())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserAccountBalanceTokenContainer);
};

export default withUserAccountBalanceTokenHoc;
