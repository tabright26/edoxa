import React, { Component } from "react";
import { connect } from "react-redux";
import { loadAddress } from "../../../../store/actions/identityActions";

const withAddressBookHoc = WrappedComponent => {
  class AddressBookContainer extends Component {
    componentDidMount() {
      this.props.actions.loadAddress();
    }

    render() {
      return <WrappedComponent addressBook={this.props.addressBook} />;
    }
  }

  const mapStateToProps = state => {
    return {
      addressBook: state.user.addressBook
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadAddress: () => dispatch(loadAddress())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(AddressBookContainer);
};

export default withAddressBookHoc;
