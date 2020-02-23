import React, { ReactNode } from "react";
import Support from "components/Shared/Support";

export type Item = {
  title?: string;
  content: ReactNode[];
};

export const items: Item[] = [
  {
    title: null,
    content: [
      <p>
        eDoxa recognizes that your privacy is serious business and we are
        committed to protecting your personal information. This website’s
        Privacy Policy (the “Privacy Policy”) describes the types of information
        collected about you when you visit the eDoxa.gg website (the “Site”),
        how this information may be used, when it may be disclosed, how you can
        take steps to control the use and disclosure of your personal
        information, and how that information is protected by us.
      </p>,
      <p>
        In this Privacy Policy, which is available via a link at the bottom of
        every page on the Site, “we”, “us” and “our” means eDoxa Inc, as well as
        its affiliates on its behalf, including eDoxa Inc (collectively,
        “eDoxa”). Any terms that are not defined in this Privacy Policy shall
        have the meaning given in the Terms and Conditions of service (the
        “T&amp;Cs”).
      </p>,
      <p>Key Definitions</p>,
      <ul>
        <li>
          “Personal Information” – generally means information about you as an
          identifiable individual, but excludes business contact information,
          specific publicly available information, and Gamer Information. It
          includes, but is not limited to: first and last name, date of birth,
          email address, gender, occupation and/or other demographic
          information.
        </li>
        <li>
          When we say “Member,” we are referring to the person or entity that is
          registered with us to use the Services. When we say “you,” we are
          referring either to a Member or to some other person who visits any of
          our Websites.
        </li>
        <li>
          We provide online platforms that allow gamers the ability to challenge
          in online tournaments, provide giveaways and related activities (the
          “Services”).
        </li>
        <li>
          “Gamer Information” means information about activity on the Site by
          you as a member which is not itself personally identifiable with you
          and is therefore not Personal Information, but which may be linked (on
          a non-identifiable basis) to your account information, and includes
          your: (a) username or alias, (b) skill rating, (c) top players ranking
          (if any) and eDoxa coins balance, (d) scores in games and tournaments,
          (e) gaming preferences and personal motto, (f) summary of gaming
          activity, including when you joined, and the number of games that you
          have played, (g) winnings from games and tournament, and (h) footage
          of gameplay in promotional materials such as trailers which may be
          displayed on the Site, or on a website run by other parties including
          those of our marketing partners (for example, “play of the week”,
          “highlight reel” or similar content).
        </li>
      </ul>,
      <p>
        This Privacy Policy applies to the Personal Information of all “users”
        of the Site, including unregistered visitors and “members” who have
        registered and maintain an account on the Site. This Privacy Policy does
        not cover any information collected through any other website operated
        by third parties, such as linked websites operated by sponsors,
        advertisers or game developers.
      </p>,
      <p>
        This Privacy Policy will remain in full force and effect with respect to
        your Personal Information which we retain for a limited period of time
        after your use or participation on the Site or any particular service,
        tournament or game offered through the Site terminates or ceases, or is
        suspended or deactivated for any reason, in order to ensure that you may
        easily restart gameplay at a later time. In any case, should you
        continue to be an inactive player for more than a reasonable time
        period, eDoxa will delete your personal information.
      </p>,
      <p>
        If you do not agree with this Privacy Policy, please do not participate
        on the Site or any of the activities or Services offered through the
        Site.
      </p>,
      <p className="mb-0">
        Finally, this Privacy Policy explains how we manage and safeguard
        Personal Information in the course of operating our business. We are
        dedicated to adhering to the following principles that relate to the
        collection, use, retention and disclosure of Personal Information.
      </p>
    ]
  },
  {
    title: "ACCOUNTABILITY",
    content: [
      <p>
        eDoxa is accountable for all Personal Information in its control. This
        includes information under the direct control of eDoxa, as well as
        Personal Information that eDoxa may transfer to its affiliates or to
        third party service providers, in each case for the purposes of (i)
        performing support functions, such as payment processing, business
        analytics, customer support, printing and mailing, data storage and
        destruction, and (ii) marketing any of eDoxa’s products and services.
        eDoxa will use contractual or other measures to require third parties
        that process information or provide services on our behalf to maintain a
        level of privacy protection comparable to our own practices.
      </p>,
      <p>
        Numerous individuals within eDoxa are responsible for the day-to-day
        collection and processing of Personal Information, including technical
        and administration personnel trained for this purpose. However, eDoxa
        has a designated Privacy Officer who is responsible for eDoxa’s
        compliance with applicable privacy legislation. Any questions or
        concerns with regard to the manner in which eDoxa handles or manages
        Personal Information, or in respect of this Privacy Policy should be
        directed to our Privacy Officer who can be reached as follows:
      </p>,
      <ul className="mb-0">
        <li>
          eDoxa Privacy Officer Email: <Support.EmailAddress />.
        </li>
      </ul>
    ]
  },
  {
    title: "COLLECTION OF INFORMATION",
    content: [
      <h4>A. What information is collected through the sites?</h4>,
      <h4>Types of Information</h4>,
      <p>
        We collect two types of information from you when you visit the Site:
        information that does not identify you personally (“Non-Personal
        Information”); and Personal Information, as defined in more detail
        below).
      </p>,
      <h5>(i) Non-Personal Information</h5>,
      <p>As examples of Non-Personal Information, we collect:</p>,
      <ul>
        <li>
          through our Web servers, automatic recordings of certain technical
          information related to your visit to the Site. This information is
          anonymous and does not identify you personally. It includes things
          such as: the Internet domain for your Internet service provider; the
          Internet Protocol (IP) address of the computer accessing the Site; the
          address of the last webpage that you visited prior to clicking through
          to the Site; the browser used and the type of computer operating
          system that your computer is using; information about your computer
          hardware; the date and time that you visited the Site; and a record of
          which pages you viewed while you were visiting the Site;
        </li>
        <li>demographic information which does not identify an individual;</li>
        <li>and Gamer Information.</li>
      </ul>,
      <h5>(ii) Personal Information</h5>,
      <p>We collect Personal Information as follows:</p>,
      <p>
        (a) Initial Registration from you, in connection with completing the
        “one step registration” required for you to create a free, basic user
        account (a “Basic Account”):
      </p>,
      <ul>
        <li>
          your email address, for the purpose of (A) verifying your identity by
          emailing you, (B) you logging in, (C) marketing upcoming Challenges to
          you, and (D) emailing game results to you;
        </li>
        <li>
          your date of birth, for the purpose of ensuring that you are permitted
          to create an account based upon your age,
        </li>
        <li>
          where you reside, for the purpose of ensuring that you are permitted
          to create an account based upon your jurisdiction,
        </li>
        <li>
          your gender, for the purpose to deliver information relevant to you,
        </li>
        <li>
          your discord username, for the purpose of marketing upcoming
          Challenges to you,
        </li>
      </ul>,
      <p>(Collectively: the “Basic Information”).</p>,
      <p>
        (b) Deposit Funds into the Basic Account: from you, for the purpose of
        creating and depositing funds to your Basic Account which will allow the
        Individual to enter cash games and tournaments (a “Real Money Account”),
        in addition to the Basic Information:
      </p>,
      <ul>
        <li>your first and last name;</li>
        <li>
          the following information with respect to your credit card: type,
          number, expiration month, expiration year and card verification
          number;
        </li>
        <li>
          The following billing information: your street address, city, postal
          code/zip code, province/state and country.
        </li>
      </ul>,
      <p>
        (c) Withdraw funds from Real Money Account: from you, for the purpose of
        confirming your identity:
      </p>,
      <ul>
        <li>your bank information, first name, last name; and</li>
        <li>
          your street address, city, postal code/zip code, province/state and
          country, and phone number.
        </li>
      </ul>,
      <p>
        (d) Withdraw funds from Real Money Account and Claiming Prizes: from
        Individuals, for the purpose of confirming their identity:
      </p>,
      <ul>
        <li>Name, age and address;</li>
      </ul>,
      <p>
        (e) Edit profile: from you, for the purpose of updating your contact
        information, your “site information”, your password, and your
        communication preferences:
      </p>,
      <ul>
        <li>
          your first and last name, Individual’s street address, city, postal
          code/zip code, province/state and country;
        </li>
        <li>
          your time zone; your avatar; and your PlayStation Network ID and XBOX
          Live Gamertag (in order for you to make or accept game challenges, and
          for us to track your game results;
        </li>
        <li>
          in response to our “About you” query, a sentence about the Individual
          for the purpose of the Individual’s profile;
        </li>
        <li>your old and new password; and</li>
        <li>your communication preferences.</li>
      </ul>,
      <p>
        (f) Game challenges (on first challenge): from you, your PlayStation
        Network ID and XBOX Live Gamertag, and Electronic Arts email address,
        other game specific identifiers, in order to make or accept game
        challenges, and for us to track your game results
      </p>,
      <p>
        The Site also provides you with certain ways to voluntarily give us your
        Personal Information, as follows:
      </p>,
      <ol>
        <li>
          from you, through your interaction with the Site (for example,
          information about which games were played, player rating information
          and player game statistics);
        </li>
        <li>
          from you, through your completion of contest forms or other
          participation in promotional efforts, for the purpose of such contests
          and promotions, including for the purpose of awarding prizes;
        </li>
        <li>
          from you, through your completion of surveys, for the purpose of
          gathering your opinions and comments in regard to eDoxa’s products and
          services;
        </li>
        <li>
          from you, through using links contained on the Site to send eDoxa an
          email message which contains such information, for the purpose
          identified at the point of collection on the Site;
        </li>
        <li>
          from you, to the extent that you post content with such Personal
          Information to chat windows, user fora or similar fora on the Site,
          for the purpose of each such forum; and
        </li>
        <li>
          from you, to the extent that you provide Personal Information when you
          contact eDoxa’s Customer Care representatives via live chat or email.
        </li>
        <li>
          from you, through you voluntarily disclosing information about
          yourself when you use the “Invite Your Friends / Tell-A-Friend”
          feature, for the purpose of inviting others to create Basic Accounts
          or Real Money Accounts;
        </li>
        <li>
          about other individuals who are identified as your “friends”, in the
          form of the names and e-mail addresses of your friends when you use
          the “Invite Your Friends / Tell-A-Friend” feature on the Site, for the
          purpose of inviting others to create Basic Accounts or Real Money
          Accounts;
        </li>
      </ol>,
      <p>
        In order to provide a fair playing environment for all users and to
        protect the integrity of the Site, we also collect Personal Information
        which we use to investigate complaints of cheating or behavior that we
        believe may be contrary to the T&amp;Cs, including the Rules of Conduct.
        This may include footage of in-game play.
      </p>,
      <p>
        In addition to collecting Personal Information directly from visitors to
        the Site, we may also collect Personal Information from third parties
        such as marketing partners to which permission has been granted to share
        that information with us.
      </p>,
      <h4>B. Other methods of gathering information</h4>,
      <strong>What are “cookies” and how does the Site use them?</strong>,
      <p>
        A cookie is a small text file containing a unique identification number
        that is transferred (through your browser) from a website to the hard
        drive of your computer. The cookie identifies your browser but will not
        let a website know any Personal Information about you, such as your name
        or address. These files are then used by such websites as a kind of
        electronic identification tag when users revisit that site. Since
        cookies are only text files, they cannot run on your computer, search
        your computer for other information, or transmit any information to
        anyone.
      </p>,
      <p>
        We and our partners may use various technologies to collect and store
        information when you use our Services, and this may include using
        cookies and similar tracking technologies on our Website, such as pixels
        and web beacons, to analyze trends, administer the website, track users’
        movements around the website, serve targeted advertisements, and gather
        demographic information about our user base as a whole. Users can
        control the use of cookies at the individual browser level. We partner
        with third parties to display advertising on our website or to manage
        and serve our advertising on other sites. Our third-party partners may
        use cookies or similar tracking technologies in order to provide you
        advertising or other content based upon your browsing activities and
        interests. If you wish to opt out of interest-based advertising click
        http://preferences-mgr.truste.com/ (or if located in the European Union
        click http://www.youronlinechoices.eu/). Please note you might continue
        to receive generic ads.
      </p>,
      <strong>What are Web beacons and how does the site use them?</strong>,
      <p>
        We use web beacons on our Websites and in our emails. When we send
        emails to Users, we may track behavior such as who opened the emails and
        who clicked the links. This allows us to measure the performance of our
        email campaigns and to improve our features for specific segments of
        Members. To do this, we include single pixel gifs, also called web
        beacons, in emails we send. Web beacons allow us to collect information
        about when you open the email, your IP address, your browser or email
        client type, and other similar details. We use the data from those Web
        Beacons to create reports about how our email campaign performed and how
        to improve our services.
      </p>,
      <strong>What about information from other sources?</strong>,
      <p>
        From time to time we may obtain information about you from third party
        sources, such as public databases, social media platforms, third party
        data providers and our joint marketing partners. We take steps to ensure
        that such third parties are legally permitted or required to disclose
        such information to us. Examples of the information we may receive from
        other sources include: demographic information, device information (such
        as IP addresses), location, and online behavioral data (such as
        information about your use of social media websites, page view
        information and search results and links). We use this information,
        alone or in combination with other information (including Personal
        Information) we collect, to enhance our ability to provide relevant
        marketing and content to you and to develop and provide you with more
        relevant products features, and services.
      </p>,
      <p>
        <h4>C. Personal Information about Children</h4>
        Our Site is intended for a general audience and we do not knowingly (a)
        allow people under 18 years of age to create accounts on the Site and
        (b) collect Personal Information from people under 18 years of age
        through our Site. In addition, in the event that a visitor to our Site
        identifies himself or herself as a person under the age of 18, we will
        not collect, store or use any Personal Information of such an
        individual. In the event that we receive Personal Information that we
        discover was provided by a person under the age of 18, we will promptly
        delete all such Personal Information not required for purpose of
        continuing to block such person’s use of the Site, and will comply with
        applicable laws, including, as applicable, the U.S. Children’s Online
        Privacy Protection Act.
      </p>,
      <p className="mb-0">
        <h4>D. Limits to Collecting Personal Information</h4>
        We limit the type of mandatory information collected by us. We collect
        only the Personal Information that is required for the purposes
        described in the following paragraphs under the heading “Use and
        Disclosure of Personal Information” below (the “Purposes”).
      </p>
    ]
  },
  {
    title: "USE AND DISCLOSURE OF PERSONAL INFORMATION",
    content: [
      <p>We use and disclose your information for the following purposes.</p>,
      <h4>A. Internal Uses</h4>,
      <p>
        We collect, use and disclose your Personal Information for such purposes
        to which you consent at the time of collection or in any case prior to
        such use, and as otherwise permitted or required by applicable law,
        including:
      </p>,
      <ul>
        <li>
          to operate our Site; to open and maintain your customer accounts with
          us, deposit funds into such accounts, and withdraw funds from such
          accounts; to edit your profile; and for the purpose of Challenges, in
          each case as more particularly described above;
        </li>
        <li>to provide a product or service requested by you;</li>
        <li>
          to contact you regarding updates to the Site, your account status, our
          new products and services and upcoming events, changes to our products
          and services;
        </li>
        <li>
          to collect subscription and entry fees from you and to process
          payments;
        </li>
        <li>
          for promotional or related purposes without additional compensation,
          including with respect to Challenges and the awarding of prizes;
        </li>
        <li>to prevent fraud and protect the integrity of the Site;</li>
        <li>to deliver prizes to you; and</li>
        <li>
          to ensure that you comply with the T&amp;Cs (including in order to
          facilitate a fair gameplay environment) and are permitted to create an
          account based upon your age and where you reside.
        </li>
        <li>
          to, in our discretion, use Personal Information to contact you
          regarding changes to the T&amp;Cs and any other policies or agreements
          relevant to the Site, including this Privacy Policy.
        </li>
      </ul>,
      <p>
        Also, if you use the “Invite Your Friends / Tell-A-Friend” feature, we
        will use the name and e-mail address that you provide to invite your
        friend to create a user account.
      </p>,
      <p>
        When you create your account and when we collect your Personal
        Information, you can customize these preferences. If you wish to change
        your account preferences to modify the ways that we contact you, please
        contact Customer Care or, in the case where we use your Personal
        Information to communicate with you via email, use the “unsubscribe”
        link included in all email messages sent on our behalf.
      </p>,
      <h4>B. Related Entities and Authorized Third-Party Service Providers</h4>,
      <p>
        In addition to sharing information between entities comprising the eDoxa
        group for the Purposes, and in order to better serve our customers, we
        may transfer Personal Information to third-party service providers who
        are legally or contractually obligated to only use the information to
        help us provide our Services and not for any other purpose. For example,
        eDoxa uses outside contractors for the purposes of (i) performing
        support functions, such as payment processing, business analytics,
        customer support, printing and mailing, data storage and destruction,
        and (ii) marketing any of eDoxa’s products and services.
      </p>,
      <p>
        From time to time such eDoxa group entities and third parties may hold,
        use or disclose your Personal Information outside of the United States
        or Canada, and while outside of United States or Canada such Personal
        Information may be accessed by regulatory authorities pursuant to the
        laws of such other jurisdictions.
      </p>,
      <p>
        These third parties may be located in the United States, Canada,
        Australia or elsewhere.
      </p>,
      <h4>C. Co-Branded Partners</h4>,
      <p>
        Co-Branded Partners are third parties with whom eDoxa may jointly offer
        a Service or feature, such as a Tournament. You can tell when you are
        accessing a Service offered by a Co-Branded Partner because the
        Co-Branded Partner’s name will be featured prominently. You may be asked
        to provide information about yourself to register for a Service offered
        by a Co-Branded Partner. In doing so, you may be providing your Personal
        Information to both us and the Co-Branded Partner, or, with your
        consent, we may share your Personal Information with the Co-Branded
        Partner. Please note that the Co-Branded Partner’s privacy policy may
        apply to its use of your Personal Information.
      </p>,
      <h4>D. Marketing Partners</h4>,
      <p>
        To the consent that you consent, we may also disclose Personal
        Information you provide us to third party marketing partners who provide
        products, information or services you have expressly requested or which
        we believe you may be interested in purchasing or obtaining, in order
        that they may provide you with information regarding such products,
        information or services. In doing so, we will make reasonable efforts to
        ensure that these third parties only use the Personal Information for
        our stated Purposes and that they provide a method of opting out from
        receiving future communications from such parties, but we shall not
        otherwise be responsible for their handling or dissemination practices
        with respect to Personal Information. If you subsequently wish to change
        your account preferences to not receive information from such
        third-party marketing partners, please contact Customer Care or use the
        “unsubscribe” link included in email messages sent by such parties.
      </p>,
      <h4>E. Legal Compliance</h4>,
      <p>
        There may be instances when eDoxa may disclose Personal Information
        without providing notice or choice, as required or permitted by
        applicable laws, which may include (i) where required pursuant to a
        subpoena, other form of legal process or request on behalf of any local,
        provincial, state, or federal governmental department or agency; (ii) in
        an emergency, to protect the safety and physical security of users of
        the Services or members of the public; or (iii) to investigate breaches
        of agreements, US or Canadian laws.
      </p>,
      <h4>F. Business Transfers</h4>,
      <p>
        If eDoxa sells all or part of its business or makes a sale or transfer
        of its assets or is otherwise involved in a merger or transfer of all or
        a material part of its business, eDoxa may transfer your Personal
        Information to the party or parties involved in the transaction as part
        of that transaction.
      </p>
    ]
  },
  {
    title: "OBTAINING CONSENT",
    content: [
      <p className="mb-0">
        In most cases, when we do collect Personal Information directly from an
        individual, the Purposes for which the individual provides us with the
        information – for example, as required for us to effectively provide our
        Services – will be clear, and as such, consent to the collection and use
        of Personal Information for those Purposes will be implied by use of the
        Services. If we choose to use Personal Information already in our
        possession for a purpose that was not identified at the time, we
        initially collected the information, we will seek the consent of the
        affected individuals before using this information for the new purpose.
        Please note that if the new purpose is required or permitted by law,
        eDoxa is not required to seek the consent of the affected individuals
        and may not do so. Individuals may withdraw consent to the collection,
        use and disclosure of their Personal Information at any time, subject to
        certain restrictions set out in applicable privacy legislation, but
        should recognize that by doing so they may be unable to utilize the
        Services and products offered on the Site. When a member uses the
        “Invite Your Friends / Tell-A-Friend” feature, we rely on that
        individual to obtain the consent of their friend to allow us to use that
        information to invite them to create a user account.
      </p>
    ]
  },
  {
    title: "MANAGEMENT OF PERSONAL INFORMATION",
    content: [
      <p>
        eDoxa has policies and procedures as to how long it retains information
        that it collects. Personal Information that is no longer required to
        fulfil the Purposes is either destroyed, erased or rendered anonymous to
        prevent unauthorized parties from gaining access to the information.
      </p>,
      <p>
        We retain Personal Information received from third parties in accordance
        with our legal and contractual obligations. The file containing your
        Personal Information will be made available to the authorized employees,
        contractors or agents of eDoxa who need to access the information in
        connection with the Purposes, and will be held primarily in an
        electronic database.
      </p>,
      <p>
        Personal Information included in computer databases is typically stored
        in a secure data center with an extra copy of the data stored at an
        alternate location of equal security to ensure that we will be able to
        retrieve the data even in the event of a disaster. These computer
        databases may be located in the United States or elsewhere in the world.
        From time to time, we may transfer Personal Information stored in these
        databases (as otherwise permitted under this Privacy Policy) to
        third-party service providers located in the United States or elsewhere
        in the world. For example, certain third-party service providers such as
        data storage companies, computer security specialists, database
        technicians, or corporate affiliates of eDoxa may have remote access to
        Personal Information in our custody for the purposes described herein,
        but we use contractual and other safeguards to ensure that Personal
        Information transferred to such parties is provided with a comparable
        level of protection as eDoxa provides, regardless of the format in which
        the information exists.
      </p>,
      <p>
        eDoxa has implemented a variety of commercially reasonable security
        safeguards to protect Personal Information in our control against loss,
        theft, misuse and interception by third parties. These safeguards
        include organizational, technical and physical measures designed to
        protect information from unauthorized access, disclosure, copying, use
        or modification. Among the steps taken are:
      </p>,
      <ul>
        <li>premises security;</li>
        <li>restricted file access to Personal Information;</li>
        <li>
          technology safeguards, including network traffic encryption, security
          software, firewalls to prevent hacking and other unauthorized computer
          access; and
        </li>
        <li>internal password and security policies.</li>
      </ul>,
      <p className="mb-0">
        However, please note that no data transmission or storage can be
        guaranteed to be 100% secure. We are committed to protecting your
        Personal information, but we cannot ensure or warrant the security of
        any information you transmit to us. Please take reasonable steps to
        guard against identity theft, including ‘phishing’ attacks. You are
        responsible for maintaining the confidentiality of your password and
        account, and you are fully responsible for all activities that occur
        under your password or account. In the event of unauthorized use of your
        password or account or any other breach of security, you must notify
        eDoxa immediately and promptly change your password.
      </p>
    ]
  },
  {
    title: "ACCURACY AND CURRENCY OF PERSONAL INFORMATION",
    content: [
      <p>
        eDoxa is committed to open and fair privacy practices which comply with
        applicable privacy legislation.
      </p>,
      <p>
        Any individual may request access to their Personal Information held by
        eDoxa by contacting eDoxa’s Privacy Officer in writing. After receiving
        such a request, our Privacy Officer shall inform the individual of the
        existence, use and disclosure of their Personal Information and shall
        allow the individual to access such information.
      </p>,
      <p>
        In certain circumstances to the extent permitted or required by
        applicable law, access to Personal Information may be denied. If we deny
        an individual’s request for access to their Personal Information, we
        will advise such individual in writing of the reason for the refusal and
        they may then challenge our decision.
      </p>,
      <p>
        Where Personal Information is used on an on-going basis, eDoxa
        undertakes to correct any information that is shown to be inaccurate,
        incomplete or not up-to-date. Otherwise, eDoxa does not routinely update
        Personal Information. It is the member’s responsibility to provide us
        current, complete, truthful and accurate information, including Personal
        Information, and to keep such information up to date. We cannot and will
        not be responsible for any problems or liability that may arise if a
        member does not provide us with accurate, truthful or complete
        information or Personal Information or the member fails to update such
        information or Personal Information.
      </p>,
      <p>
        Upon notification from an individual of their desire to have any or all
        of their Personal Information in our records deleted or destroyed, eDoxa
        will undertake to delete or destroy all such information to the extent
        that retention of such information is not required or permitted by
        applicable law, such as for the investigation of claims or the defense
        of an action, and some Personal Information may remain in back-up
        storage for this purpose. If you wish to delete your data, you can
        contact support via <Support.EmailAddress />.
      </p>,
      <p className="mb-0">
        In the event that an individual wishes to challenge eDoxa’s compliance
        with any of these principles, they can contact eDoxa’s Privacy Officer
        using the contact information set out at the outset of this Privacy
        Policy. If eDoxa finds a complaint to be justified, appropriate measures
        will be taken, including, if necessary, amending our policies and
        practices.
      </p>
    ]
  },
  {
    title: "GOVERNING",
    content: [
      <p>
        The Site is governed by and operated in accordance with the provincial
        laws of Quebec and federal laws of Canada applicable therein, without
        regard to its conflict of law provisions, and you hereby consent to the
        exclusive jurisdiction of and venue in the courts of the province of
        Quebec, in Canada, in all disputes arising out of or relating to the
        Services. Notwithstanding any other provisions of this Policy or the
        T&amp;Cs, we may seek injunctive or other equitable relief from any
        court of competent jurisdiction. You agree that any dispute that cannot
        be resolved between the parties shall be resolved individually, without
        resort to any form of class action.
      </p>,
      <p className="mb-0">
        We make no representation that this Site is operated in accordance with
        the laws or regulations of, or governed by, other nations. By utilizing
        the Services and participating in Site activities, you certify that you
        meet the age and other eligibility requirements for the Site and the
        Services set forth in the T&amp;Cs. If you do not meet the age and other
        eligibility requirements, please discontinue using the Site and the
        Services immediately as your continued use of the Site and the Services
        indicates that you are agreeing to the collection, use, disclosure,
        management and storage of your Personal Information as described in this
        Privacy Policy.
      </p>
    ]
  },
  {
    title: "CHANGES TO PRIVACY POLICY",
    content: [
      <p className="mb-0">
        All policies and procedures of eDoxa are reviewed and updated
        periodically to reflect changes in our practices and Services, including
        this Privacy Policy. If we modify this Privacy Policy, we will update
        the “effective Date” and such changes will be effective upon posting to
        the Site, to the extent that such changes do not require that we first
        obtain your consent to same.
      </p>
    ]
  }
];
