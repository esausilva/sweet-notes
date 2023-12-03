import styles from './SpecialSomeones.module.scss';

export function SpecialSomeones(): JSX.Element {
  return (
    <section className={styles.specialSomeones}>
      <h2>Special Someones</h2>
      <div className={styles.specialSomeoneDropdownWrapper}>
        <select>
          <option value="volvo">Volvo</option>
          <option value="saab">Saab</option>
          <option value="mercedes">Mercedes</option>
          <option value="audi">Audi</option>
        </select>
        <p className={styles.annotation}>
          <a href="#">Add Special Someone</a>
        </p>
      </div>
      <form className={styles.noteForm} action="">
        <label>Note</label>
        <textarea name="note"></textarea>
        <p className={styles.annotation}>10 / 50</p>
        <button className="button--primary" type="submit">
          Send Note
        </button>
      </form>
      <p>
        Share this unique link with your <em>Special Someone</em> to access your
        notes to them:
      </p>
      <p>
        <a href="#">
          https://domain.com/ss/jmvCFalPuA~nlGp2xnVD_smUdnwV5U)fUmoa51uTJtFQ_
        </a>
      </p>
      <p className={styles.annotation}>
        Note: Anyone with the <em>Special Someone</em> link will be able to see
        the notes!
      </p>
    </section>
  );
}
